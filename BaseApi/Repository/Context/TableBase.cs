namespace PGP.Repository.Context;

public abstract class TableBase
{
    public int Id { get; set; }
    public bool Ativo { get; set; } = true;
    public string UsuarioCriacao { get; set; }
    public DateTime DataCriacao { get; set; }
    public string? UsuarioAlteracao { get; set; }
    public DateTime? DataAlteracao { get; set; }
}