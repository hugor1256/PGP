using AutoMapper;
using PGP.Entities;
using PGP.Records;

namespace PGP.Repository.Mapper;

public class UsuariosMapper : Profile
{
    public UsuariosMapper()
    {
        CreateMap<Usuario, UsuariosRecord>();
    }
}