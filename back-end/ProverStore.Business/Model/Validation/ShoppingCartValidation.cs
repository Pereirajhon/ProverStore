/*
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Model.Validation
{
    public class ShoppingCartValidation: AbstractValidator<ShoppingCart>
    {
        public ShoppingCartValidation() 
        {
            RuleForEach(c => c.CartItens).SetValidator(new ShoppingCartItemValidation());

        } 
    }
}
*/