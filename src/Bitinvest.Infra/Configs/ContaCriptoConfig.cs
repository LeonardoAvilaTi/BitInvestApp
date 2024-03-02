using Bitinvest.Domain.Entities.ContaCripto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bitinvest.Infra.Configs
{
    public class ContaCriptoConfig : IEntityTypeConfiguration<ContaCripto>
    {
        public void Configure(EntityTypeBuilder<ContaCripto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.CriptoMoeda)
                .IsRequired()
                .HasColumnType("varchar(30)");

            builder.Property(x => x.DescricaoOperacao)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(x => x.DataMovimento)
                .IsRequired();

            builder.Property(x => x.ClienteId)
                .IsRequired();


            builder.Property(x => x.TipoOperacao)
               .IsRequired()
               .HasColumnType("smallint");

            builder.Property(x => x.Quantidade)
                .IsRequired()
                .HasColumnType("decimal(18,4)");

            builder.Property(x => x.Saldo)
                .IsRequired()
                .HasColumnType("decimal(18,4)");

            builder.Property(x => x.Aprovado)
               .IsRequired();

            builder.Ignore(x => x.ValidationResult);

            builder.ToTable("MovimentoContaCripto");
        }
    }
}
