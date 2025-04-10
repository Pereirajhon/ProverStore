using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProverStore.Business.Interface;
using ProverStore.Business.Model;
using ProverStore.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>,IClienteRepository
    {
        private readonly DbSet<Cliente> _cliente;
        private readonly AppDbContext _db;
        
        public ClienteRepository(AppDbContext db)
        {
            _db = db;
            _cliente = db.Set<Cliente>();
           
        }
        
        public async Task AddEnderecoCliente(EnderecoCliente endereco)
        {
            _db.EnderecoClientes.Add(endereco);

            await _db.SaveChangesAsync();
        }

        public async Task<Cliente> BuscarClientePorId(Guid clienteId)
        {
            return await _cliente.FindAsync(clienteId);
        }

        
        
    }
}
