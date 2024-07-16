using Microsoft.AspNetCore.Http.Features;
using PGP.Repository.Repositories;
using PGP.Services;
using PGP.Startup.Base;

namespace PGP.Startup;

public class FolderInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)

    {
        services.Configure<FormOptions>(x =>
        {
            x.ValueLengthLimit = int.MaxValue;
            x.MultipartBodyLengthLimit = long.MaxValue;
        });

        // services
        services.AddScoped<UsuariosService>();
        
        // repositories
        services.AddScoped<UsuariosRepository>();
    }
}