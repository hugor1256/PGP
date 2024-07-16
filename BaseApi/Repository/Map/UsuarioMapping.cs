using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PGP.Entities;

namespace PGP.Repository.Map;

public class UsuarioMapping : TableBaseMapping<Usuario>
{
    public override void Configure(EntityTypeBuilder<Usuario> builder)
    {
        SetTabela("Usuarios", true);
        
        base.Configure(builder);

        builder.Property(u => u.Cpf).HasMaxLength(11).IsRequired();
        builder.Property(u => u.Login).HasMaxLength(200).IsRequired();
        builder.Property(u => u.Senha).HasMaxLength(200).IsRequired();
    }
}