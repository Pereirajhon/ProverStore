using System.ComponentModel.DataAnnotations;

namespace ProverStore.Api.ViewModel
{
    public class UserVM
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string? Nome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Senha { get; set; }
        [Compare("Senha", ErrorMessage = "A senha não confere")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? ConfirmaSenha { get; set; }
    }

    public class LoginVM
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(36, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string? Senha { get; set; }
    }
   
}
