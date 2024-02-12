using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaExpress.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity Find(Object Id);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);

        void Delete(Object Id);

        int SaveChanges();

    }
}
