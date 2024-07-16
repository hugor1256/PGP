using Microsoft.EntityFrameworkCore;
using PGP.Entities;
using PGP.Repository.Map;

namespace PGP.Repository.Context;

public class PgpContext : DbContext
{
    public PgpContext(DbContextOptions<PgpContext> opts) :base(opts) { }

    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioMapping());
    }
}