using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProverStore.Business.Model.Pedido;

namespace ProverStore.Business.Model.Validation
{
    public class PedidoValidation: AbstractValidator<Pedido>
    {
        public PedidoValidation()
        {
            RuleFor(c => c.ClienteId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(pedido => pedido.PedidoId)
           .NotEmpty().WithMessage("O ID do item do pedido é obrigatório.");

            RuleFor(pedido => pedido.PedidoList)
                .NotEmpty().WithMessage("A lista de itens do pedido não pode estar vazia.");
            RuleFor(pedido => pedido.DataPedido)
                .GreaterThan(DateTime.MinValue).WithMessage("A data do pedido é obrigatória.");

            RuleFor(c => c.TotalValor).GreaterThan(0).WithMessage("O Total do Pedido deve ser maior que 0");

            RuleForEach(p => p.PedidoList).SetValidator(new PedidoItemValidation());
        }
    }
}
