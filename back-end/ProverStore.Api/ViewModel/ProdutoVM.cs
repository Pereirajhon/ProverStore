using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using ProverStore.Business.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProverStore.Api.ViewModel
{
    public class GetProdutoVM
    {
        [Key]
        public Guid Id { get; set; }

        public string? Categoria { get; set; }

        public string? Marca { get; set; }

        public string? Modelo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Descricao { get; set; }
       // public IFormFile? Imagem { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string ImagemProduto { get; set; }
        public double Valor { get; set; }
        public int Favoritados { get; set; }
        public IEnumerable<Guid>? ClienteIdsFavorito { get; set; } 
        public int Estoque { get; set; }
        public bool Ativo { get; set; }
    }

    public class ProdutoVM
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Categoria { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Marca { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Modelo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string ImagemProduto { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public double Valor { get; set; }
        public int Favoritados { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Estoque { get; set; }
        public bool Ativo { get; set; }
    }

    public class UpdateProdutoVM
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Categoria { get; set; }
        //    public Guid CategoriaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Marca { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Modelo { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? Descricao { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string? ImagemProduto { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public double Valor { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Estoque { get; set; }
        public bool Ativo { get; set; }
    }
}
