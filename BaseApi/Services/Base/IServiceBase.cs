using FluentSGI.Interfaces;

namespace PGP.Services.Base;

public interface IServiceBase : INotify
{
    /// <summary>
    /// Guardar as informações do usuário autenticado. (utilizar na controller)
    /// </summary>
    /// <param name="usuario"></param>
    void SetUsuario(string? usuario = null);

    /// <summary>
    /// Guardar o perfil do usuário autenticado (utilizar na controller)
    /// </summary>
    /// <param name="perfil"></param>
    void SetPerfil(string? perfil = null);
        
    /// <summary>
    /// Guardar o número do cpf do usuário autenticado (utilizar na controller)
    /// </summary>
    /// <param name="cpf"></param>
    void SetCPF(string? cpf = null);

    /// <summary>
    /// Obtem o nome do usuário (utilizar na service)
    /// </summary>
    /// <returns></returns>
    string? GetNomeUsuario();

    /// <summary>
    /// Obtem o perfil do usuário (utilizar na service)
    /// </summary>
    /// <returns></returns>
    string? GetPerfilUsuario();

    /// <summary>
    /// Obtem o número de CPF do usuário (utilizar na service)
    /// </summary>
    /// <returns></returns>
    string? GetCPF();
    
}