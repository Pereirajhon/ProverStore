using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Categoria)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Marca)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Modelo)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(1000)");

           // builder.HasOne(p => p.Categoria)
           //     .WithMany(p => p.Produtos)
           //     .HasForeignKey(p => p.CategoriaId);

            builder.Property(p => p.Valor)
                .IsRequired()
            .HasColumnType("decimal(18, 2)");

            builder.Property(p => p.ImagemProduto)
                .IsRequired()
            .HasMaxLength(255);

            //  builder.Property(p => p.Favoritados)
            //      .IsRequired();

            builder.HasMany(p => p.Favoritos)
                .WithOne(p => p.Produto)
                .HasForeignKey(p => p.ProdutoId);

            builder.Property(p => p.Estoque)
                .HasDefaultValue(1)
                .IsRequired();

            builder.ToTable("Produtos");
        }
    }
}
