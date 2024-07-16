using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PGP.Repository.Context;

namespace PGP.Repository.Map
{
    public class TableBaseMapping<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : TableBase
    {
        private string _nomeDaTabela = "";
        private bool _isTemporal;

        public void SetTabela(string nomeDaTabela = "", bool? isTemporal = false)
        {
            _nomeDaTabela = nomeDaTabela;
            _isTemporal = isTemporal.Value;
        }

        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            if (!string.IsNullOrEmpty(_nomeDaTabela))
            {
                if (_isTemporal)
                {
                    builder.ToTable(_nomeDaTabela, s => s.IsTemporal(t =>
                    {
                        t.UseHistoryTable($"{_nomeDaTabela}Auditoria");
                        t.HasPeriodStart($"{_nomeDaTabela}DataInicio");
                        t.HasPeriodEnd($"{_nomeDaTabela}DataFim");
                    }));
                }
                else
                {
                    builder.ToTable(_nomeDaTabela);
                }
            }

            builder.HasKey(d => d.Id);
            builder.Property(s => s.Ativo).IsRequired();
            builder.Property(s => s.UsuarioCriacao).HasMaxLength(200).IsRequired();
            builder.Property(s => s.DataCriacao).IsRequired();
            builder.Property(s => s.UsuarioAlteracao).HasMaxLength(200).IsRequired(false);
            builder.Property(s => s.DataAlteracao).IsRequired(false);
        }
    }
}

