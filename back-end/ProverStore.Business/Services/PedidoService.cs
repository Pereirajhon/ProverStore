using Microsoft.AspNetCore.Builder;
using ProverStore.Business.Interface;
using ProverStore.Business.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Services
{
    public class PedidoService: IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IUnitOfWork _uow;
        
        public PedidoService(IPedidoRepository pedidoRepository, IProdutoService produtoService, IUnitOfWork uow)
        {
            _pedidoRepository = pedidoRepository;            
            _produtoService = produtoService;
            _uow = uow;

        }
        public async Task AddPedido(Pedido pedido)
        {
           
            foreach (var item in pedido.PedidoList)
            {

                var produto = await _produtoService.ObterPorId(item.ProdutoId);
                if (produto == null) return;
                produto.Estoque -= item.Quantidade;

                await _produtoService.Atualizar(produto);
            }

            await _pedidoRepository.CriarPedido(pedido);

            await _uow.Commit();

        }
    }
}
