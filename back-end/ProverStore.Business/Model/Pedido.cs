using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Model
{
    public class Pedido: Entity
    {
        public Guid ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public Guid PedidoId { get; set; }
        public ICollection<PedidoItem> PedidoList { get; set; } = new List<PedidoItem> { };
        public DateTime DataPedido { get; set; }
        public double TotalValor { get; set; }
    }
}
