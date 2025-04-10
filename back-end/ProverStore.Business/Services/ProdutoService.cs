using ProverStore.Business.Interface;
using ProverStore.Business.Model;
using ProverStore.Business.Model.Validation;
using ProverStore.Business.Notificacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        private IProdutoRepository _produtoRepository;
        public ProdutoService(IProdutoRepository produtoRepository, INotificador notificador):base(notificador)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Adicionar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;
            if(await _produtoRepository.ProdutoExiste(produto))
            {
               Notificar("Já existe produto com esse mesmo modelo !");
                return;
            }
            await _produtoRepository.Adicionar(produto);
        }

        public async Task Atualizar(Produto produto)
        {
            if (!ExecutarValidacao(new ProdutoValidation(), produto)) return;
            

            await _produtoRepository.Atualizar(produto);
        }

        public async Task<Produto> ObterPorId(Guid id)
        {
           return await _produtoRepository.ObterPorId(id);
            
        }

        public async Task Deletar(Guid id)
        {
            await _produtoRepository.Remover(id);
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }

        
    }
}
