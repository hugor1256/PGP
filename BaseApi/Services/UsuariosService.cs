using AutoMapper;
using PGP.Records;
using PGP.Repository.Repositories;

namespace PGP.Services;

public class UsuariosService
{

    private readonly UsuariosRepository _usuariosRepository;
    private readonly IMapper _mapper;

    public UsuariosService(UsuariosRepository usuariosRepository, IMapper mapper)
    {
        _usuariosRepository = usuariosRepository;
        _mapper = mapper;
    }

    public async Task<UsuariosRecord> RetornarUsuarioPorId(int id)
    {
        var usuario = _usuariosRepository.ObterPorId(id);

        return _mapper.Map<UsuariosRecord>(usuario);
    }
}