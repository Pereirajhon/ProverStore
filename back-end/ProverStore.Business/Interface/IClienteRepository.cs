using Microsoft.AspNetCore.Identity;
using ProverStore.Business.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Interface
{
    public interface IClienteRepository: IRepository<Cliente> 
    {
       public Task AddEnderecoCliente(EnderecoCliente endereco);
       public Task<Cliente> BuscarClientePorId(Guid clienteId);

        public Task AtualizarEnderecoCliente(Cliente cliente);

    }
}
