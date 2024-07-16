using PGP.Entities;
using PGP.Repository.Context;
using PGP.Repository.Repositories.Base;

namespace PGP.Repository.Repositories;

public class UsuariosRepository : RepositoryBase<Usuario>
{
    public UsuariosRepository(PgpContext pgpContext) : base(pgpContext)
    {
    }
}