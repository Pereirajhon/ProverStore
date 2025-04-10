/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Model
{
    public class ShoppingCart: Entity
    {
        public Guid ShoppingCartId { get; set; }
        public ICollection<ShoppingCartItem>? CartItens { get; set; }
        public double Total { get;set; } 
        public ShoppingCart()
        {
            CartItens = new List<ShoppingCartItem> { };
            Total = CartItens.Sum(c => c.TotalProduto);
        }
    }
}

*/