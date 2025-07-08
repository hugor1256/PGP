using System.Security.Claims;
using AutoMapper;
using FluentSGI.Notification;
using PGP.Domain;
using PGP.Entities;
using PGP.Helpers;
using PGP.Records;
using PGP.Repository.Repositories;
using PGP.Services.Base;

namespace PGP.Services;

/// <summary>
/// Service de funcionalidades relacionadas ao usuario (UsuarioController)
/// </summary>
public class UsuariosService : ServiceBase
{

    private readonly UsuariosRepository _usuariosRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Injeção de dependencia
    /// </summary>
    /// <param name="usuariosRepository"></param>
    /// <param name="mapper"></param>
    public UsuariosService(UsuariosRepository usuariosRepository, IMapper mapper)
    {
        _usuariosRepository = usuariosRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Retorna os dados do usuario por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ListarUsuarioRecord RetornarUsuarioPorId(int id)
    {
            var usuario = _usuariosRepository.ObterPorId(id);
        
            AddNotifications(new Validation().Required()
                .Null(usuario, "usuario", "Usuario não encontrado")
            );

            if (Invalid())
                return null;
        
            return _mapper.Map<ListarUsuarioRecord>(usuario);
    }

    /// <summary>
    /// Cadastra um novo usuário
    /// </summary>
    /// <param name="usuarioRecord"></param>
    public void CadastrarUsuario(CadastrarUsuarioRecord usuarioRecord)
    {
            AddNotifications(new Validation().Required()
                .IsTrue(string.IsNullOrEmpty(usuarioRecord.Cpf), "CPF", "Informe o CPF do usuario")
                .IsTrue(string.IsNullOrEmpty(usuarioRecord.Login), "Login", "Login o CPF do usuario")
                .IsNotCPF(usuarioRecord.Cpf)
                .IsLess(usuarioRecord.Cpf.Trim().Length, 11, "CPF", "Formato do CPF inválido")
                .IsTrue(usuarioRecord.Senha.IsStrongPassword(), "Senha", "Senha fraca")
                .IsNotEqual(usuarioRecord.Senha, usuarioRecord.ConfirmarSenha, "Senha/ConfimarSenha","As senhas não coincidem"));

            if (Invalid()) 
                return;

            var novoUsuario = _mapper.Map<Usuario>(usuarioRecord);
            
            novoUsuario.UsuarioCriacao = "Hugo";
            novoUsuario.DataCriacao = DateTime.Today;
            novoUsuario.Ativo = true;

            _usuariosRepository.Inserir(novoUsuario);
            _usuariosRepository.Salvar();
    }

    public async Task<List<Claim>> LogarUsuario(LogarUsuarioRecord usuario)
    {
        var user = _usuariosRepository.ObterPorCpf(usuario.Cpf.SomenteNumeros());
        
        AddNotifications(new Validation().Required()
            .Null(user,"user","Usuário não encontrado")
            .ValidateAll()
            .IsNotCPF(usuario.Cpf, "CPF", "CPF informado invalido")
            .IsNotEqual(usuario.Senha.CriptografarSenha(), user.Senha,"senha","Senha inválida")
        );

        if (Invalid())
            return new List<Claim>();
        
        return new List<Claim>
        {
            new Claim("CPF", user.Cpf),
            new Claim("Login", user.Login),
            new Claim("Perfis", PerfilEnum.Usuario.GetDescription()),
        };
    }


    public void ManipularDictionary(List<DicRecord> record)
    {
        var dic = record.FirstOrDefault()!.agr.ToDictionary(t => t.chave, t => t.valor);
        var usuario = _usuariosRepository.ObterTodos().ToList();

        foreach (var u in usuario)
        {
            if (!dic.ContainsKey(u.Cpf))
                dic.Add(u.Cpf, u.Login);
        }
    }
}