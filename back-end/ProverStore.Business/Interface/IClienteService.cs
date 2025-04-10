using ProverStore.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Interface
{
    public interface IClienteService
    {
        public Task AdicionarEnderecoCliente(EnderecoCliente enderecoCliente);
        public Task AtualizarEnderecoCliente(Cliente cliente);
    }
}
