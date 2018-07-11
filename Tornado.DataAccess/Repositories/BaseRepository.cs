using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Tornado.DataAccess.Extensions;
using Tornado.DataAccess.Helpers;
using Tornado.DataAccess.Interfaces;
using Tornado.Domain.Interfaces;

namespace Tornado.DataAccess.Repositories
{
    public abstract class BaseRepository<T, U> : IRepository<T> where T : class, IEntity, new() where U : DbContext, new()
    {

        protected abstract DbSet<T> DbSet(U entityContext);

        protected abstract Expression<Func<T, bool>> IdentifierPredicate(U entityContext, Guid id);
        public U EntityContext;

        protected BaseRepository()
        {
            EntityContext = new U();
        }

        #region helper methods

        private T AddEntity(T entity)
        {
            return DbSet(EntityContext).Add(entity);
        }

        private IEnumerable<T> GetEntities()
        {
            return DbSet(EntityContext).ToFullyLoaded();
        }

        private T GetEntity(Guid id)
        {
            return DbSet(EntityContext).Where(IdentifierPredicate(EntityContext, id)).FirstOrDefault();
        }

        private T UpdateEntity(T entity)
        {
            var q = DbSet(EntityContext).Where(IdentifierPredicate(EntityContext, entity.Id));
            return q.FirstOrDefault();
        }

        #endregion

        public T Create(T entity)
        {
            var addedEntity = AddEntity(entity);
            EntityContext.SaveChanges();
            return addedEntity;
        }

        public IEnumerable<T> GetAll()
        {
            return (GetEntities()).ToArray().ToList();
        }

        public int GetTotal()
        {
            return DbSet(EntityContext).Count();
        }
        
        public T Get(Guid id)
        {
            return GetEntity(id);
        }

        public ICollection<T> Where(Expression<Func<T, bool>> match)
        {
            return DbSet(EntityContext).Where(match).ToList();
        }

        public T Update(T entity)
        {
            var existingEntity = UpdateEntity(entity);

            MapperHelper.PropertyMap(entity, existingEntity);
            
            EntityContext.SaveChanges();
            
            return existingEntity;
        }

        public void Delete(Guid id)
        {
            var entity = GetEntity(id);
            EntityContext.Entry(entity).State = EntityState.Deleted;
            EntityContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            EntityContext.Entry(entity).State = EntityState.Deleted;
            EntityContext.SaveChanges();
        }

        public void DeleteAll()
        {
            DbSet(EntityContext).RemoveRange(DbSet(EntityContext));
            EntityContext.SaveChanges();
        }
    }
}
