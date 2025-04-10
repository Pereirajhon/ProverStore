using ProverStore.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Interface
{
    public interface IShoppingCartRepository
    {
        Task AddAoCarrinho(Guid id);
        Task Decrementar(Guid id);
        Task RemoverDoCarrinho(Guid carrinhoId);
        Task EsvaziarCarrinho();
        Task<IEnumerable<ShoppingCartItem>> ObterListaCarrinho();
    }
}
