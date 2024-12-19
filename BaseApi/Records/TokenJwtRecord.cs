using System.Text.Json.Serialization;
namespace PGP.Records;

/// <summary>
/// Classe de autenticação do JWT Token
/// </summary>
public class TokenJwtRecord
{
    [JsonPropertyName("secret")]
    public string Secret { get; set; }

    [JsonPropertyName("issuer")]
    public string Issuer { get; set; }

    [JsonPropertyName("audience")]
    public string Audience { get; set; }

    [JsonPropertyName("accessExpiration")]
    public int AccessExpiration { get; set; }

    [JsonPropertyName("refreshExpiration")]
    public int RefreshExpiration { get; set; }

    [JsonPropertyName("isExpiration")]
    public bool IsExpiration { get; set; } = true;
}

/// <summary>
/// Resultado do Token gerado
/// </summary>
public class TokenResult
{
    public string tokenString { get; set; }
    public double tokenExpiresIn { get; set; }
}