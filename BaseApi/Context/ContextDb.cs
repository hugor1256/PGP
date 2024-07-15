using PGP.Models;
using Microsoft.EntityFrameworkCore;

namespace PGP.Context;

public class ContextDb : DbContext
{
    public ContextDb(DbContextOptions<ContextDb> opts) :base(opts) { }

    public DbSet<Usuario> Usuario { get; set; }
}