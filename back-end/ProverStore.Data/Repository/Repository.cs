using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using ProverStore.Business.Interface;
using ProverStore.Business.Model;
using ProverStore.Data.Context;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProverStore.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly DbSet<TEntity> DbSet;
        protected readonly AppDbContext Db;

        protected Repository(AppDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }
        public async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await Db.SaveChangesAsync();
        }

        public async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await Db.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

       // public async Task<List<TEntity>> ObterTodos()
       // {
       //     return await DbSet.ToListAsync();
      //  }

        public async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task Remover(Guid id)
        {
           await DbSet.FindAsync(new TEntity { Id = id});
            await Db.SaveChangesAsync();
        }

       
        public async Task SaveChanges()
        {
            await Db.SaveChangesAsync();
        }
    }
}
