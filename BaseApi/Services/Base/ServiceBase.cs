using FluentSGI.Notification;

namespace PGP.Services.Base;

public class ServiceBase : Notify, IServiceBase
{
    private string _usuarioNome;
    private string _usuarioPerfil;
    private string _cpf;

    public void SetUsuario(string? usuario = null) => _usuarioNome = usuario ?? "Usuário da Aplicação";
    public void SetCPF(string? cpf = null) => _cpf = cpf ?? "";
    public void SetPerfil(string? perfil = null) => _usuarioPerfil = perfil ?? "Perfil";
    
    
    public string GetNomeUsuario() => _usuarioNome;
    public string GetPerfilUsuario() => _usuarioPerfil;
    public string? GetCPF() => _cpf;

}