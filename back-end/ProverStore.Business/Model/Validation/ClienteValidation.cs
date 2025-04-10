using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Model.Validation
{
    public class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation() {
           // RuleFor(cliente => cliente.Nome)
           //    .NotEmpty().WithMessage("O nome do cliente é obrigatório.")
           //    .MinimumLength(3).WithMessage("O Nome deve ter mais de 3 caracteres.")
           //    .MaximumLength(100).WithMessage("O Nome deve ter no máximo 100 caracteres.");
            /*
            RuleFor(cliente => cliente.CPF)
                .NotEmpty().WithMessage("O CPF do cliente é obrigatório.")
                .Length(11).WithMessage("O CPF do cliente deve ter exatamente 11 dígitos.")
                .Must(BeNumeric).WithMessage("O CPF do cliente deve conter apenas números.");

            RuleFor(cliente => cliente.Telefone)
                .NotEmpty().WithMessage("O telefone do cliente é obrigatório.")
                .Must(BeNumeric).WithMessage("O telefone deve conter apenas números.")
                .MinimumLength(8).WithMessage("O telefone deve ter mais de 8 caracteres")
                .MaximumLength(18).WithMessage("O telefone deve ter no máximo 18 caracteres.");
            */
            RuleFor(cliente => cliente.Email)
                .NotEmpty().WithMessage("O e-mail do cliente é obrigatório.")
                .MinimumLength(6).WithMessage("O e-mail deve ter mais do que 6 caracteres.")
                .MaximumLength(255).WithMessage("O e-mail do cliente deve ter no máximo 255 caracteres.")
                .EmailAddress().WithMessage("O e-mail não é válido.");
        }
        //private bool BeNumeric(string value)
       // {
      //      return long.TryParse(value, out _);
       // }
    }
}
