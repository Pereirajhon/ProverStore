using ProverStore.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Interface
{
    public interface IProdutoService: IDisposable
    {
        Task<Produto> ObterPorId(Guid id);
        Task Adicionar(Produto produto);
        Task Atualizar(Produto produto); 
        Task Deletar(Guid id);
    }
}
