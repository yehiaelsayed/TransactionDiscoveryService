using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactionDiscovery.Data.Models;

namespace TransactionDiscovery.Data.Repository
{
    public interface IRepository<T> where T : BaseModel
    {
        T Insert(T entity);
        void Insert(List<T> entities);

        IQueryable<T> Select();

        T Find(Guid Id);

        void Delete(T entity);

        void Delete(List<T> entities);

        void Update(T entity);

        void Update(List<T> entities);

        int SaveChanges();

    }
}
