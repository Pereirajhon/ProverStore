using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Model
{
    public class Categoria: Entity
    {
        public Guid CategoriaId { get; set; }
        public string? NomeCategoria { get; set; }
        public Guid ProdutoId { get; set; }
        public List<Produto>? Produtos { get; set; }

    }
}
