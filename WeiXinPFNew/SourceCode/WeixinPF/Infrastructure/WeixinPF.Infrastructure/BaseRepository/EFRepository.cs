using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using WeixinPF.Application.BaseRepository;

namespace WeixinPF.Infrastructure.BaseRepository
{
    public class EFRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public EFRepository(DbContext dbContext)
        {
            Context = dbContext;
        }

        public DbContext Context { get; protected set; }

        /// <summary>
        ///     新增操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Add(TEntity entity)
        {
            var state = Context.Entry(entity).State;

            if (state == EntityState.Detached)
            {
                Context.Entry(entity).State = EntityState.Added;
            }

            Context.SaveChanges();

            return true;
        }

        /// <summary>
        ///     更新操作
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Update(TEntity entity)
        {
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;

            return Context.SaveChanges() > 0;
        }

        /// <summary>
        ///     批量更新
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public bool Update(IEnumerable<TEntity> entities)
        {
            try
            {
                Context.Configuration.AutoDetectChangesEnabled = false;

                using (var scope = new TransactionScope())
                {
                    foreach (var entity in entities)
                    {
                        Update(entity);
                    }

                    scope.Complete();
                }

                return true;
            }
            finally
            {
                Context.Configuration.AutoDetectChangesEnabled = true;
            }
        }

        /// <summary>
        ///     查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate != null)
                return Context.Set<TEntity>().Where<TEntity>(predicate);
            return Context.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetReturnEnumerable(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate != null)
                return Context.Set<TEntity>().Where<TEntity>(predicate);
            return Context.Set<TEntity>();
        }

        /// <summary>
        ///     带分页的查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="conditions"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Get(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> predicate)
        {
            var skinCount = (pageIndex - 1)*pageSize;
            if (predicate != null)
                return Context.Set<TEntity>()
                    .Where<TEntity>(predicate)
                    .Skip(skinCount)
                    .Take(pageSize);
            return Context.Set<TEntity>()
                .Skip(skinCount)
                .Take(pageSize);
        }
    }
}