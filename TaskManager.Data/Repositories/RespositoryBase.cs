using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using TaskManager.Data.Model;
using TaskManager.Data.Repositories.Interfaces;

namespace TaskManager.Data.Repositories
{
    public class RepositoryBase<TEntity> : IDisposable, IRepositoryBase<TEntity> where TEntity : class
    {
        protected TaskManagerEntities Db = new TaskManagerEntities();

        public void Add(TEntity obj)
        {
            Db.Set<TEntity>().Add(obj);
            Db.SaveChanges();
           
        }

        public TEntity GetById(int id)
        {

            var entity = Db.Set<TEntity>().Find(id);
            Db.Entry<TEntity>(entity).Reload();

            return entity;

            //return  Db.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Db.Set<TEntity>().AsNoTracking().ToList();
        }

        public void Update(TEntity obj)
        {
            Db.Entry(obj).State = EntityState.Modified;
            Db.SaveChanges();
            
        }

        public void Remove(TEntity obj)
        {

            bool saveFailed;
            do
            {
                saveFailed = false;
                try
                {
                    Db.Set<TEntity>().Remove(obj);
                    Db.Entry(obj).State = EntityState.Deleted;
                    Db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;
                    ex.Entries.Single().Reload();
                }
            }while (saveFailed);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Reload(TEntity obj)
        {
            Db.Entry<TEntity>(obj).Reload();
            
        }

    }

}
