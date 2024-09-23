using Microsoft.AspNetCore.Mvc;
using PGP.Archteture;
using PGP.Controllers.Base;
using PGP.Records;
using PGP.Services;

namespace PGP.Controllers;

[Route("Usuarios")]
public class UsuariosController : PgpController
{
    /// <summary>
    /// Retorna os dados do usuario por Id
    /// </summary>
    /// <param name="service"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiResponse<ListarUsuario>), 200)]
    [ProducesResponseType(typeof(ApiResponse<string>), 400)]
    [ProducesResponseType(typeof(ApiResponse<string>), 500)]
    [HttpGet("RetornarUsuarioPorId/{id:int}")]
    public IActionResult Get([FromServices] UsuariosService service, int id)
    {
        try
        {
            var usuario = service.RetornarUsuarioPorId(id);
            
            if (service.Invalid())
                return BadRequest(ApiResponse<string>.Fail(string.Join("|", service.NotificationsListMenssages())));
            
            return Ok(ApiResponse<ListarUsuario>.Success(usuario));
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
    /// <param name="usuario"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiResponse<string>), 200)]
    [ProducesResponseType(typeof(ApiResponse<string>), 400)]
    [ProducesResponseType(typeof(ApiResponse<string>), 500)]
    [HttpPost("CadastrarUsuario")]
    public IActionResult Post([FromServices] UsuariosService service, [FromBody] CadastrarUsuario usuario)
    {
        try
        {
            service.CadastrarUsuario(usuario);
            
            if (service.Invalid())
                return BadRequest(ApiResponse<string>.Fail(string.Join("|", service.NotificationsListMenssages())));
            
            return Ok(ApiResponse<string>.Success("Usuário criado com sucesso"));

        }
        catch (Exception e)
        {
            return StatusCode(500, ApiResponse<string>.Fail(e.Message));
        }
    }

}