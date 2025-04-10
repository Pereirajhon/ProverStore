using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProverStore.Business.Interface;
using ProverStore.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Data.Context
{
    public class AppDbContext: IdentityDbContext<Cliente,IdentityRole<Guid>, Guid>
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Favorito> Favoritos { get; set;}
     //   public DbSet<ShoppingCartItem> ShoppingCart { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItems { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<EnderecoCliente> EnderecoClientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
               foreach (var property in modelBuilder.Model.GetEntityTypes()
                   .SelectMany(e => e.GetProperties()
                     .Where(p => p.ClrType == typeof(string))))
                   property.SetColumnType("varchar(100)");

               modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

             foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            
            modelBuilder.Entity<Favorito>()
               .HasKey(f => new { f.ClienteId, f.ProdutoId });
            
            modelBuilder.Entity<Favorito>()
                .HasOne(p => p.Cliente)
                .WithMany(p => p.Favoritos)
                .HasForeignKey(p => p.ClienteId);
            
            modelBuilder.Entity<Favorito>()
                .HasOne(p => p.Produto)
                .WithMany(p => p.Favoritos)
                .HasForeignKey(p => p.ProdutoId);
            
            
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
       

    }
}

