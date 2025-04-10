using ProverStore.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Interface
{
    public interface IFavoritoRepository
    {
        public Task<IEnumerable<Produto>> ObterProdutoFavoritadoUsuario(Guid clienteId);
        public Task Favoritar(Favorito favorito);
        public Task DesFavoritar(Favorito favorito);
        public Task SaveChanges();
    }
}
