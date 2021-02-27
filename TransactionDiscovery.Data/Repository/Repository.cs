using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TransactionDiscovery.Data.Context;
using TransactionDiscovery.Data.Models;
using TransactonDiscovery.Utils;

namespace TransactionDiscovery.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private TransactionDiscoveryDbContext _dbContext;
        public Repository(TransactionDiscoveryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Delete(T entity)
        {
            try
            {
                _dbContext.Remove(entity);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }
        }

        public void Delete(List<T> entities)
        {
            try
            {
                _dbContext.RemoveRange(entities);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }
        }

        public T Find(Guid Id)
        {
            try
            {
                return _dbContext.Set<T>().Find(Id);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }
        }

        public T Insert(T entity)
        {
            try
            {
                entity.RecoredCreationDate = DateTime.UtcNow;
                return _dbContext.Set<T>().Add(entity).Entity;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }

        }

        public void Insert(List<T> entities)
        {
            try
            {
                entities.ForEach(e => e.RecoredCreationDate = DateTime.UtcNow);
                _dbContext.Set<T>().AddRange(entities);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }
        }

        public int SaveChanges()
        {
            try
            {
                return _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }
        }

        public IQueryable<T> Select()
        {
            try
            {
                return _dbContext.Set<T>();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }
        }

        public void Update(T entity)
        {
            try
            {
                entity.RecoredUpdateDate = DateTime.UtcNow;
                _dbContext.Update(entity);
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
                throw ex;
            }
        }

        public void Update(List<T> entities)
        {
            try
            {
                entities.ForEach(e => e.RecoredUpdateDate = DateTime.UtcNow);
                _dbContext.UpdateRange(entities);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                throw ex;
            }
        }

    }
}
