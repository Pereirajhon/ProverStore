using Microsoft.EntityFrameworkCore;
using ProverStore.Business.Interface;
using ProverStore.Business.Model;
using ProverStore.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Data.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext db): base(db)
        {
        }
        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await Db.Produtos.AsNoTracking()
                .Include(p => p.Favoritos)
                .ToListAsync();

        }
        public async Task<IEnumerable<Produto>> BuscarPorFiltro(string query)
        {
            var produtoQuery = await Db.Produtos.AsNoTracking()
                .Where(p => p.Modelo.Contains(query) || p.Categoria.Contains(query))
                .ToListAsync();

            return produtoQuery;
        }
      
       public async Task<IEnumerable<Produto>> ObterProdutosPorCategoria(Categoria categoria)
       {
            return await Db.Produtos.Where(c => c.Categoria == categoria.NomeCategoria).ToListAsync();
       }

        public async Task<bool> ProdutoExiste(Produto produto)
        {
           return await Db.Produtos.AnyAsync(p => p.Modelo == produto.Modelo);
            
        }

    }
}
