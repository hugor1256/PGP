using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PGP.Archteture;
using PGP.Controllers.Base;
using PGP.Domain;
using PGP.Helpers;
using PGP.Records;
using PGP.Services;

namespace PGP.Controllers;

[Route("PgpApi/Usuarios")]
[ApiController]
[ApiConventionType(typeof(DefaultApiConventions))]
public class UsuariosController : PgpController
{
    /// <summary>
    /// Retorna os dados do usuario por Id
    /// </summary>
    /// <param name="service"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiResponse<ListarUsuarioRecord>), 200)]
    [ProducesResponseType(typeof(ApiResponse<string>), 400)]
    [ProducesResponseType(typeof(ApiResponse<string>), 500)]
    [HttpGet("RetornarUsuario/{id:int}")]
    public IActionResult Get([FromServices] UsuariosService service, int id)
    {
        try
        {
            var usuario = service.RetornarUsuarioPorId(id);
            
            if (service.Invalid())
                return BadRequest(ApiResponse<string>.Fail(string.Join("|", service.NotificationsListMenssages())));
            
            return Ok(ApiResponse<ListarUsuarioRecord>.Success(usuario));
        }
        catch (Exception e)
        {
            return StatusCode(500, ApiResponse<string>.Fail(e.Message));
        }
    }

    /// <summary>
    /// Retorna os dados do usuario por Id
    /// </summary>
    /// <param name="service"></param>
    /// <param name="usuarioRecord"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiResponse<string>), 200)]
    [ProducesResponseType(typeof(ApiResponse<string>), 400)]
    [ProducesResponseType(typeof(ApiResponse<string>), 500)]
    [HttpPost("CadastrarUsuario")]
    public IActionResult Post([FromServices] UsuariosService service, [FromBody] CadastrarUsuarioRecord usuarioRecord)
    {
        try
        {
            service.CadastrarUsuario(usuarioRecord);
            
            if (service.Invalid())
                return BadRequest(ApiResponse<string>.Fail(string.Join("|", service.NotificationsListMenssages())));
            
            return Ok(ApiResponse<string>.Success("Usuário criado com sucesso"));

        }
        catch (Exception e)
        {
            return StatusCode(500, ApiResponse<string>.Fail(e.Message));
        }
    }

    /// <summary>
    /// Loga o Usuario na aplicação
    /// </summary>
    /// <param name="tokenManagement"></param>
    /// <param name="usuario"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiResponse<string>), 200)]
    [ProducesResponseType(typeof(ApiResponse<string>), 400)]
    [ProducesResponseType(typeof(ApiResponse<string>), 500)]
    [HttpPost("LogarUsuario")]
    public async Task<IActionResult> GetDados([FromServices] IOptions<TokenJwtRecord> tokenManagement, [FromServices] UsuariosService service, [FromBody] LogarUsuarioRecord usuario)
    {
        try
        {
            var logarUsuario = await service.LogarUsuario(usuario);
        
            if (service.Invalid())
                return BadRequest(ApiResponse<string>.Fail(string.Join("|", service.NotificationsListMenssages())));

            var tokenResult = new TokenAuthenticationService(tokenManagement).GerarTokenAcesso(logarUsuario);

            return Ok(ApiResponse<object>.Success(new
            {
                acessoPermitido = true,
                nomeUsuario = usuario.Cpf,
                tokenResult.tokenString,
                tokenResult.tokenExpiresIn,
                perfil = PerfilEnum.Usuario.GetDescription()
            }));
        }
        catch (Exception e)
        {
            return StatusCode(500, ApiResponse<string>.Fail(e.Message));
        }
    }
    
    /// <summary>
    /// Retorna os dados do usuario por Id
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiResponse<string>), 200)]
    [ProducesResponseType(typeof(ApiResponse<string>), 400)]
    [ProducesResponseType(typeof(ApiResponse<string>), 500)]
    [HttpPost]
    public IActionResult Get([FromServices] UsuariosService service, [FromBody] List<DicRecord> record)
    {
        try
        {
          service.ManipularDictionary(record);
            
        return Ok(ApiResponse<string>.Success("OK"));

        }
        catch (Exception e)
        {
            return StatusCode(500, ApiResponse<string>.Fail(e.Message));
        }
    }
}