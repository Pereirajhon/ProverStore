using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Model
{
    public class Produto: Entity
    {
        public string? Categoria { get; set; } 
        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? Descricao { get; set; }
        public string? ImagemProduto { get; set; }
        public double Valor { get; set; }
        public IEnumerable<Favorito>? Favoritos { get; set; }
        public int Favoritados { get; set; }
        public int Estoque { get; set; }
        public bool Ativo { get; set; }
        public Produto()
        {
            if(Estoque == 0)
            {
                Ativo = false;
            }

        }
    }
}
