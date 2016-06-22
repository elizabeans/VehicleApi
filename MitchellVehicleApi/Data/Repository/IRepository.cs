using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MitchellVehicleApi.Data.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        // Create
        TEntity Add(TEntity entity);

        // Read
        TEntity GetById(object id);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> where);
        int Count();
        int Count(Func<TEntity, bool> where);
        bool Any(Expression<Func<TEntity, bool>> where);

        // Update
        void Update(TEntity entity);

        // Delete
        void Delete(object id);
        void Delete(TEntity entity);
    }
}
