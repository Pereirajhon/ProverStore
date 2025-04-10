using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using ProverStore.Business.Interface;
using ProverStore.Business.Model;
using ProverStore.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Data.Repository
{
    [Authorize]
    public class FavoritoRepository : IFavoritoRepository
    {
        private readonly AppDbContext _db;
        private readonly IProdutoRepository _produtoRepository;
        private readonly DbSet<Favorito> _favoritos;
        
        public FavoritoRepository(AppDbContext db, IProdutoRepository produtoRepository)
        {
            _db = db;
            _favoritos = db.Set<Favorito>();
            _produtoRepository = produtoRepository;
        }
        public async Task<IEnumerable<Produto>> ObterProdutoFavoritadoUsuario(Guid clienteId)
        {
            
            var produtosFavoritados = await (from f in _db.Favoritos
                                                 join p in _db.Produtos on f.ProdutoId equals p.Id
                                                 where f.ClienteId == clienteId
                                                 select p)
                                     .ToListAsync();
            
            return produtosFavoritados;
        }
        
        public async Task Favoritar(Favorito favorito)
        {
             _favoritos.Add(favorito);
            await _db.SaveChangesAsync();
        }

        public async Task DesFavoritar(Favorito favorito)
        {
           // var favoritado = await _favoritos.FindAsync(favorito);
           // if (favoritado == null) return;

            _favoritos.Remove(favorito);
            await _db.SaveChangesAsync();
        }
        
        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }

        
    }
}
