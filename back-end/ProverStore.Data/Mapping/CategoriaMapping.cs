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
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.NomeCategoria).IsRequired();

         //   builder.HasMany(c => c.Produtos)
         //       .WithOne(c => c.Categoria)
         //       .HasForeignKey(c => c.CategoriaId);
        }
    }
}
