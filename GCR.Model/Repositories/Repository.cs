using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCR.Core.Repositories;

namespace GCR.Model.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DB context;

        public Repository()
        {
            context = new DB();
        }

        public Repository(DbConnection existingConnection)
        {
            context = new DB(existingConnection);
        }

        public virtual IQueryable<TEntity> Query
        {
            get
            {
                return context.Set<TEntity>();
            }
        }

        public virtual void Create(TEntity item)
        {
            
            context.Set<TEntity>().Add(item);
        }

        public virtual void Update(TEntity item)
        {
            context.Entry<TEntity>(item).State = System.Data.EntityState.Modified;
        }

        public virtual void Delete(TEntity item)
        {
            context.Set<TEntity>().Remove(item);
        }

        public virtual void SaveChanges()
        {
            context.SaveChanges();
        }
    }
}
