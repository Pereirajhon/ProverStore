using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Model.Validation
{
    public class ShoppingCartItemValidation: AbstractValidator<ShoppingCartItem>
    {
        public ShoppingCartItemValidation() 
        {
            RuleFor(c => c.Item).SetValidator(new ProdutoValidation());
            RuleFor(c => c.TotalProduto).GreaterThan(0).WithMessage("O Total do Produto deve ser maior que 0");
        }
    }
}
