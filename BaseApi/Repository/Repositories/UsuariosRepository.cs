using PGP.Entities;
using PGP.Repository.Context;
using PGP.Repository.Repositories.Base;

namespace PGP.Repository.Repositories;

public class UsuariosRepository : RepositoryBase<Usuario>
{
    public UsuariosRepository(PgpContext pgpContext) : base(pgpContext)
    {
    }

    /// <summary>
    /// Obtem o ususario pelo CPF
    /// </summary>
    /// <param name="cpf"></param>
    /// <returns></returns>
    public Usuario? ObterPorCpf(string cpf) => ObterPorPredicato(u => u.Cpf == cpf).FirstOrDefault();
}