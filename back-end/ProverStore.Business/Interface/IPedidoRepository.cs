using ProverStore.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Interface
{
    public interface IPedidoRepository
    {
        public Task<IEnumerable<Pedido>> ObterTodosPedidos();
        public Task<IEnumerable<Pedido>> ObterPedidosPorUsuario(Guid clienteId);
        public Task CriarPedido(Pedido pedido);
    }
}
