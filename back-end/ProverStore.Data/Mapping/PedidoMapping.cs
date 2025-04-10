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
    public class PedidoMapping : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.DataPedido)
                .IsRequired();

            builder.HasMany(p => p.PedidoList)
                .WithOne(p => p.Pedido)
                .HasForeignKey(p => p.PedidoId);

            builder.HasOne(p => p.Cliente)
                .WithMany(p => p.Pedidos)
                .HasForeignKey(p => p.PedidoId);

            builder.Property(p => p.TotalValor)
                .HasColumnType("decimal(18, 2)")
                .IsRequired();
        }
    }
}
