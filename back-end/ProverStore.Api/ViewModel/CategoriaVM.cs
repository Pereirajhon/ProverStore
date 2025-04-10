using ProverStore.Business.Model;
using System.ComponentModel.DataAnnotations;

namespace ProverStore.Api.ViewModel
{
    public class CategoriaVM
    {
        public Guid CategoriaId { get; set; }
        [Required(ErrorMessage = "O Campo {0} é Obrigatório ")]
        public string? CategoriaNome { get; set; }
        public Guid ProdutoId { get; set; }
        public List<ProdutoVM>? ProdutoList { get; set; } //= new List<ProdutoVM> { };

        
    }
}
