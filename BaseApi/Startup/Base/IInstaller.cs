namespace PGP.Startup.Base;

public interface IInstaller
{
    void InstallServices(IServiceCollection services, IConfiguration configuration);
}