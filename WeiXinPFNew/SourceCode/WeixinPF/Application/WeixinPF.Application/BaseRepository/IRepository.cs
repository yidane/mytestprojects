using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace WeixinPF.Application.BaseRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        bool Add(TEntity entity);

        bool Update(TEntity entity);

        bool Update(IEnumerable<TEntity> entities);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        IQueryable<TEntity> Get(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> predicate);
    }
}