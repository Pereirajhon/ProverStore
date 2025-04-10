using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Model.Validation
{
    public class ProdutoValidation : AbstractValidator<Produto>
    {
        public ProdutoValidation() 
        {

        //    RuleFor(produto => produto.CategoriaId)
        //        .NotEqual(Guid.Empty)
         //       .WithMessage("Id do Categoria inválido");

         //   RuleFor(produto => produto.Categoria)
         //       .NotEmpty().WithMessage("O campo {PropertyName} é Obrigatório ");
                
            RuleFor(produto=> produto.Categoria)
                .NotEmpty().WithMessage("O campo {PropertyName} é Obrigatório ")
                 .Length(2, 20).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(produto => produto.Marca)
               .NotEmpty().WithMessage("O campo {PropertyName} é Obrigatório ")
                .Length(2, 20).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(produto => produto.Modelo)
               .NotEmpty().WithMessage("O campo {PropertyName} é Obrigatório ")
               
                .Length(2, 24).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(produto => produto.Descricao)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 1500).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(produto => produto.Valor)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .GreaterThan(1).WithMessage("Valor deve ser maior que 1");

            RuleFor(produto => produto.ImagemProduto)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");

          //  RuleFor(x => x.Ativo)
          //      .NotNull().WithMessage("O campo Ativo deve ser preenchido.");

            RuleFor(produto => produto.Estoque)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .GreaterThanOrEqualTo(0).WithMessage("O Estoque do Produto não pode ser zero")
                .LessThan(4).When(produto => produto.Estoque < 4)
                .WithMessage("O estoque está baixo, considere repor.");

        }

    }
}
