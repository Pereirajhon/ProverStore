using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Model
{
    public class ShoppingCartItem : Entity
    {
        public Guid ShoppingCartId { get; set; }
        public Guid ProdutoId { get; set; }
        public Produto Item { get; set; }
        public int Quantidade { get; set; } 
        public double TotalProduto { get; set; } 

       
        
    }
}
