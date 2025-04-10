/*
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
    public class ShoppingCartMapping : IEntityTypeConfiguration<ShoppingCart>
    {
        public void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            builder.ToTable("Carrinho");

            builder.HasKey(c => c.Id);

            builder.HasMany(c => c.CartItens)
                .WithOne()
                .HasForeignKey(c => c.ShoppingCartId);

            builder.Property(c => c.Total)
                .IsRequired();
        }
    }
}

*/