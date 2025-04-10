
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProverStore.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Data.Mapping
{
    public class ShoppingCartItemMapping: IEntityTypeConfiguration<ShoppingCartItem>
    { 

        public void Configure(EntityTypeBuilder<ShoppingCartItem> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Quantidade).IsRequired();

            builder.Property(c => c.TotalProduto).IsRequired();

            builder.HasOne(c => c.Item)
                .WithMany()
                .HasForeignKey(c => c.ProdutoId);

            builder.ToTable("CarrinhoItens");
                
                
        }
    }
}
