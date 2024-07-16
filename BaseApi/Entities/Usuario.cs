using PGP.Repository.Context;

namespace PGP.Entities;

public class Usuario : TableBase
{
    public string Cpf { get; set; }
    public string Login { get; set; }
    public string Senha { get; set; }
}