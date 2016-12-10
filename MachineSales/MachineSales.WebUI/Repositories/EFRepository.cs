using MachineSales.WebUI.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MachineSales.WebUI.Repositories
{
    public class EFRepository
    {
        private traktorsSalesDbEntities context;
        public EFRepository()
        {
            context = new traktorsSalesDbEntities();
        }
        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            //context.Entry(entity).State = EntityState.Deleted;
            context.Set<TEntity>().Attach(entity);
            context.Set<TEntity>().Remove(entity);
            context.SaveChanges();
        }

        public List<TEntity> Get<TEntity>(Func<TEntity, bool> criteria = null) where TEntity : class
        {
            IQueryable<TEntity> query = context.Set<TEntity>();
            if (criteria != null)
            {
                query = query.Where(criteria).AsQueryable();
            }
            return query.ToList();
        }

        public TEntity GetSingle<TEntity>(Func<TEntity, bool> criteria) where TEntity : class
        {
            IQueryable<TEntity> query = context.Set<TEntity>();
            query = query.Where(criteria).AsQueryable();
            return query.SingleOrDefault();
        }

        public TEntity Insert<TEntity>(TEntity entity) where TEntity : class
        {
            context.Set<TEntity>().Add(entity);
            context.SaveChanges();
            return entity;
        }

        public TEntity Update<TEntity>(TEntity entity) where TEntity : class
        {
            context.Entry<TEntity>(entity).State = EntityState.Modified;
            context.SaveChanges();
            return entity;
        }
    }
}