using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProverStore.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Data.Mapping
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.UserName).HasColumnType("VARCHAR(80)").IsRequired();
            
                builder.HasMany(c => c.Favoritos)
                .WithOne(c => c.Cliente)
                .HasForeignKey(c => c.ClienteId); 

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnType("nvarchar(256)");

            builder.HasMany(c => c.Pedidos)
                .WithOne(c => c.Cliente)
                .HasForeignKey(c => c.ClienteId);

            builder.HasOne(c => c.EnderecoCliente)
                .WithOne(c => c.Cliente);
        }
    }
}
