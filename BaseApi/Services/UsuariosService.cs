using AutoMapper;
using FluentSGI.Notification;
using PGP.Entities;
using PGP.Helpers;
using PGP.Records;
using PGP.Repository.Repositories;
using PGP.Services.Base;

namespace PGP.Services;

public class UsuariosService : ServiceBase
{

    private readonly UsuariosRepository _usuariosRepository;
    private readonly IMapper _mapper;

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
    public ListarUsuario RetornarUsuarioPorId(int id)
    {
        try
        {
            var usuario = _usuariosRepository.ObterPorId(id);
        
            AddNotifications(new Validation().Required()
                .Null(usuario, "usuario", "Usuario não encontrado")
            );

            if (Invalid())
                return null;
        
            return _mapper.Map<ListarUsuario>(usuario);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Cadastra um novo usuário
    /// </summary>
    /// <param name="usuario"></param>
    public void CadastrarUsuario(CadastrarUsuario usuario)
    {
        try
        {
            
            AddNotifications(new Validation().Required()
                .IsTrue(string.IsNullOrEmpty(usuario.Cpf), "CPF", "Informe o CPF do usuario")
                .IsTrue(string.IsNullOrEmpty(usuario.Login), "Login", "Login o CPF do usuario")
                .IsNotCPF(usuario.Cpf)
                .IsLess(usuario.Cpf.Trim().Length, 11, "CPF", "Formato do CPF inválido")
                .IsTrue(usuario.Senha.IsStrongPassword(), "Senha", "Senha fraca")
                .IsNotEqual(usuario.Senha, usuario.ConfirmarSenha, "Senha/ConfimarSenha","As senhas não coincidem"));

            if (Invalid()) 
                return;

            var novoUsuario = _mapper.Map<Usuario>(usuario);
            
            novoUsuario.UsuarioCriacao = "Hugo";
            novoUsuario.DataCriacao = DateTime.Today;
            novoUsuario.Ativo = true;

            _usuariosRepository.Inserir(novoUsuario);
            _usuariosRepository.Salvar();
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}