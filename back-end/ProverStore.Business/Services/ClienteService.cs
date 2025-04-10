using ProverStore.Business.Interface;
using ProverStore.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Services
{
    public class ClienteService: BaseService,IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository, INotificador notificador):base(notificador)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task AdicionarEnderecoCliente(EnderecoCliente enderecoCliente)
        {

            await _clienteRepository.AddEnderecoCliente(enderecoCliente);

        }


        
    }
}
