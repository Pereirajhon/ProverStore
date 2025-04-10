
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ProverStore.Api.ViewModel;
using ProverStore.Business.Interface;

namespace ProverStore.Api.Controllers
{
    [Authorize]
    [Route("api/pedidos")]
    public class PedidoController: MainController
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;       
        public PedidoController(IPedidoRepository pedidoRepository, IMapper mapper ,INotificador notificador) : base(notificador)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IEnumerable<PedidoVM>> GetTodosPedidos()
        {
           var todosPedidos = await _pedidoRepository.ObterTodosPedidos();
            return _mapper.Map<IEnumerable<PedidoVM>>(todosPedidos);

        }

        [Authorize]
        [HttpGet("{clienteId}:guid")]
        public async Task<IEnumerable<PedidoVM>> GetPedidoCliente(Guid clienteId)
        {
            var pedidosCliente = await _pedidoRepository.ObterPedidosPorUsuario(clienteId);

            if (pedidosCliente == null) CustomResponse();

            return _mapper.Map< IEnumerable<PedidoVM>>(pedidosCliente);
        }

    }
}
