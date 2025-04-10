using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Model
{
    public class Favorito 
    {
        public Guid ProdutoId { get; set; }
        public Guid ClienteId { get; set; }
        public Produto Produto { get; set; }
        public Cliente Cliente {get ; set;}
    }
}
