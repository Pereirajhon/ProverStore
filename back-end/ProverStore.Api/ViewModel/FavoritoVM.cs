using System.ComponentModel.DataAnnotations;

namespace ProverStore.Api.ViewModel
{
    public class FavoritoVM
    {
        public Guid ClienteId { get; set;}
        public Guid ProdutoId { get; set;}
    }
}
