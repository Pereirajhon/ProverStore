using System.ComponentModel.DataAnnotations;

namespace ProverStore.Api.ViewModel
{
    public class ClienteVM
    {
        [Key]
        public Guid Id { get; set; }
        public string? Email { get; set; }
        public string? Role { get; set; }
        public EnderecoClienteVM? EnderecoClienteVM { get; set; }

    }
}
