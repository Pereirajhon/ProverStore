using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Model
{
    public class PedidoItem: Entity
    {
        public Guid PedidoId { get; set; }
        public Pedido Pedido { get; set; }
        public Guid ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public Double Valor { get; set; }
    }
}
