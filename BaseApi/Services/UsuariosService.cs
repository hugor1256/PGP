using AutoMapper;
using FluentSGI.Notification;
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
    public async Task<UsuariosRecord> RetornarUsuarioPorId(int id)
    {
        var usuario = _usuariosRepository.ObterPorId(id);
        
        AddNotifications(new Validation().Required()
            .Null(usuario, "usuario", "Usuario não encontrado")
        );

        if (Invalid())
            return null;
        
        return _mapper.Map<UsuariosRecord>(usuario);
    }
}