using ProverStore.Business.Interface;
using ProverStore.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Data.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Commit()
        {
            return await _db.SaveChangesAsync() > 0;
        }
         
        public Task Rollback()
        {
            return Task.CompletedTask;
        }
    }
}
