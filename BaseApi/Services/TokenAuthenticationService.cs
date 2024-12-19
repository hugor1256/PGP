using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PGP.Records;

public class TokenAuthenticationService
{
    private readonly TokenJwtRecord _tokenJwtRecord;

    public TokenAuthenticationService(IOptions<TokenJwtRecord> tokenManagement)
    {
        _tokenJwtRecord = tokenManagement.Value;
    }

    public TokenResult GerarTokenAcesso(List<Claim> claim)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenJwtRecord.Secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        claim.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claim.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64));
        
        var expirationDate = _tokenJwtRecord.IsExpiration
            ? DateTime.Now.AddMinutes(_tokenJwtRecord.AccessExpiration)
            : DateTime.MaxValue;

        var jwtToken = new JwtSecurityToken(
            issuer: _tokenJwtRecord.Issuer,
            audience: _tokenJwtRecord.Audience,
            claims: claim,
            expires: expirationDate,
            signingCredentials: credentials
        );

        string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        var secondsUntilExpiration = (_tokenJwtRecord.IsExpiration 
            ? expirationDate.ToUniversalTime() - DateTime.UnixEpoch 
            : TimeSpan.MaxValue).TotalSeconds;

        return new TokenResult
        {
            tokenString = token,
            tokenExpiresIn = Math.Floor(secondsUntilExpiration)
        };
    }
}