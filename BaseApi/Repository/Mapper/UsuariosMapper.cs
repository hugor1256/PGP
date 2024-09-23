using AutoMapper;
using PGP.Entities;
using PGP.Helpers;
using PGP.Records;

namespace PGP.Repository.Mapper;

public class UsuariosMapper : Profile
{
    public UsuariosMapper()
    {
        CreateMap<Usuario, ListarUsuario>();
        
        CreateMap<CadastrarUsuario, Usuario>()
            .ForMember(s => s.Cpf, d => d.MapFrom<string>(s => s.Cpf.SomenteNumeros()))
            .ForMember(s => s.Senha, d => d.MapFrom<string>(s => s.Senha.EncryptPassword()));
    }
}