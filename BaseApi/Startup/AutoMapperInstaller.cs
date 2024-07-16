using PGP.Startup.Base;

namespace PGP.Startup;

public class AutoMapperInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}