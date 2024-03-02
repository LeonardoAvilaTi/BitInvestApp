using Bitinvest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bitinvest.Infra.Configs
{
    public class ClienteConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Nome);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.OwnsOne(x => x.Cpf, cpf => {

                cpf.Property(c => c.Numero)
                    .HasColumnName("Cpf")
                    .IsRequired()
                    .HasColumnType("varchar(11)");
            });

            builder.OwnsOne(x => x.Endereco, end => {

                end.Property(c => c.Logradouro)
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                end.Property(c => c.Numero)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                end.Property(c => c.Cep)
                    .IsRequired()
                    .HasColumnType("varchar(8)");

                end.Property(c => c.Complemento)
                    .HasColumnType("varchar(250)");

                end.Property(c => c.Bairro)
                    .IsRequired()
                    .HasColumnType("varchar(150)");

                end.Property(c => c.Cidade)
                    .IsRequired()
                    .HasColumnType("varchar(150)");

                end.Property(c => c.Estado)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            });


            builder.OwnsOne(x => x.Telefone, tel => {

                tel.Property(c => c.Numero)
                    .HasColumnName("NumeroTelefone")
                    .IsRequired()
                    .HasColumnType("varchar(15)");
            });

            builder.Ignore(x => x.ValidationResult);

            builder.ToTable("Clientes");
        }
    }
}
