using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCR.Core.Repositories
{
    public interface IRepository
    {
        void SaveChanges();
    }
    
    public interface IRepository<TEntity> : IRepository where TEntity : class
    {
        IQueryable<TEntity> Query { get; }

        void Create(TEntity item);

        void Update(TEntity item);

        void Delete(TEntity item);
    }
}
