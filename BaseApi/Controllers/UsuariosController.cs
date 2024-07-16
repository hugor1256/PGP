using Microsoft.AspNetCore.Mvc;
using PGP.Archteture;
using PGP.Records;
using PGP.Services;

namespace PGP.Controllers;

public class UsuariosController : ControllerBase
{
    [ProducesResponseType(typeof(ApiResponse<UsuariosRecord>), 200)]
    [ProducesResponseType(typeof(ApiResponse<string>), 400)]
    [ProducesResponseType(typeof(ApiResponse<string>), 500)]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromServices] UsuariosService service, int id)
    {
        try
        {
            var usuario = await service.RetornarUsuarios(id);
            
            return Ok(ApiResponse<UsuariosRecord>.Success(usuario));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return null;

    }
}