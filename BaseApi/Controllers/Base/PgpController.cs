using Microsoft.AspNetCore.Mvc;
using PGP.Services.Base;

namespace PGP.Controllers.Base;

public class PgpController : ControllerBase
{
    protected void SetUsuario(IServiceBase service)
    {
        if (User.Claims.Any(x => x.Type.Equals("Nome")))
            service.SetUsuario(User.Claims.Where(x => x.Type.Equals("Nome", StringComparison.Ordinal)).First().Value);
        else
            throw new Exception("Acesso não Autorizado!");
    }

    protected void SetPerfil(IServiceBase service)
    {
        if (User.Claims.Any(x => x.Type.Equals("PerfilAtivo")))
            service.SetPerfil(User.Claims.Where(x => x.Type.Equals("PerfilAtivo", StringComparison.Ordinal)).First().Value);
        else
            throw new Exception("Acesso não Autorizado!");
    }

    protected void SetCPF(IServiceBase service)
    {
        if (User.Claims.Any(x => x.Type.ToUpper().Equals("CPF")))
            service.SetCPF(User.Claims.Where(x => x.Type.ToUpper().Equals("CPF", StringComparison.Ordinal)).First().Value);
        else
            throw new Exception("Acesso não Autorizado!");
    }
}