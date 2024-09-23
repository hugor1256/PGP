using Microsoft.AspNetCore.Mvc;
using PGP.Archteture;
using PGP.Controllers.Base;
using PGP.Records;
using PGP.Services;

namespace PGP.Controllers;

public class UsuariosController : PgpController
{
    /// <summary>
    /// Retorna os dados do usuario por Id
    /// </summary>
    /// <param name="service"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(ApiResponse<UsuariosRecord>), 200)]
    [ProducesResponseType(typeof(ApiResponse<string>), 400)]
    [ProducesResponseType(typeof(ApiResponse<string>), 500)]
    [HttpGet("Usuarios/RetornarUsuarioPorId/{id:int}")]
    public async Task<IActionResult> Get([FromServices] UsuariosService service, int id)
    {
        try
        {
            var usuario = await service.RetornarUsuarioPorId(id);
            
            if (service.Invalid())
                return BadRequest(ApiResponse<string>.Fail(string.Join("|", service.NotificationsListMenssages())));
            
            return Ok(ApiResponse<UsuariosRecord>.Success(usuario));
        }
        catch (Exception e)
        {
            return StatusCode(500, ApiResponse<string>.Fail(e.Message));
        }
    }
}