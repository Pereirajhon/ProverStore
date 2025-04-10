using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Model.Validation
{
    public class PedidoItemValidation: AbstractValidator<PedidoItem>
    {
        public PedidoItemValidation() 
        {

            RuleFor(item => item.ProdutoId)
                .NotEmpty().WithMessage("O ID do produto do item do pedido é obrigatório.");

            RuleFor(c => c.Valor)
                .GreaterThan(0).WithMessage("O Valor do item do pedido deve ser maior que zero.");

        }
    }
}
