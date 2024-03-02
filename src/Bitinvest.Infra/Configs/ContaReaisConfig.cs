using Bitinvest.Domain.Entities.ContaReais;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bitinvest.Infra.Configs
{
    public class ContaReaisConfig : IEntityTypeConfiguration<ContaCorrenteReais>
    {
        public void Configure(EntityTypeBuilder<ContaCorrenteReais> builder)
        {
            builder.HasKey(x => x.Id);
            
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

            builder.Property(x => x.Valor)
                .IsRequired()
                .HasColumnType("decimal(18,4)");

            builder.Property(x => x.Saldo)
                .IsRequired()
                .HasColumnType("decimal(18,4)");

            builder.Ignore(x => x.ValidationResult);

            builder.ToTable("MovimentoContaReais");
        }
    }
}
