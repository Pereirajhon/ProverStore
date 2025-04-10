using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Business.Interface
{
    public interface IUnitOfWork
    { 
        public Task<bool> Commit();
        public Task Rollback();
    }
}
