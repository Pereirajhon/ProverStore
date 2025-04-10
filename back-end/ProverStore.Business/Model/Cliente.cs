using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Model
{
    public class Cliente : IdentityUser<Guid>
    {
        public Guid FavoritoId { get; set; }
        public Guid EnderecoClienteId { get; set; }
        public ICollection<Favorito>? Favoritos { get; set; }
        public IEnumerable<Pedido>? Pedidos { get; set; }
        public Guid EnderecoClienteId {get; set;}
        public EnderecoCliente? EnderecoCliente { get; set; }
    }
}
