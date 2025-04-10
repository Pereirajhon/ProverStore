using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using ProverStore.Api.ViewModel;
using ProverStore.Business.Interface;
using ProverStore.Business.Model;
using System.CodeDom;
using System.Drawing.Text;

namespace ProverStore.Api.Controllers
{
    [Authorize]
    [Route("api/cliente")]
    public class EnderecoClienteController: MainController
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;
        public EnderecoClienteController(IClienteRepository clienteRepository, IMapper mapper, INotificador notificador) : base(notificador)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        [HttpPut("endereco")]
        public async Task<ActionResult> EnderecoCliente(EnderecoClienteVM enderecoVM)
        {
            if (enderecoVM == null) return CustomResponse();
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var cliente = await _clienteRepository.BuscarClientePorId(enderecoVM.ClienteId);
            if (cliente == null) return BadRequest("Cliente não encontrado");

            var enderecoMapeado = _mapper.Map< EnderecoCliente>(enderecoVM);
            
                        
            await _clienteRepository.AddEnderecoCliente(enderecoMapeado);
            return CustomResponse(enderecoVM);

        }
        
    }
}
