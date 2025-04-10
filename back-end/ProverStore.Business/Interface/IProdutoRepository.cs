using ProverStore.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Interface
{
    public interface IProdutoRepository: IRepository<Produto>   
    {
        Task<IEnumerable<Produto>> BuscarPorFiltro(string query);
        Task<IEnumerable<Produto>> ObterTodos();
        Task<IEnumerable<Produto>> ObterProdutosPorCategoria(Categoria categoria);
        Task<bool> ProdutoExiste(Produto produto);
    }
}
