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
    public class PedidoRepository: IPedidoRepository
    {
        private readonly AppDbContext _db;
        private readonly DbSet<Pedido> _pedido;
        public PedidoRepository(AppDbContext db ) 
        { 
            _db = db;
            _pedido = db.Set<Pedido>();
        }

        public async Task CriarPedido(Pedido pedido)
        {
             _pedido.Add(pedido);
            await _db.SaveChangesAsync();

        }
        public async Task<IEnumerable<Pedido>> ObterTodosPedidos()
        {
            return await _pedido.ToListAsync();
        }
        public async Task<IEnumerable<Pedido>> ObterPedidosPorUsuario(Guid clienteId)
        {

            return await _pedido.Where(p => p.ClienteId == clienteId).Include(p => p.PedidoList).ToListAsync();

        }
    }
}
