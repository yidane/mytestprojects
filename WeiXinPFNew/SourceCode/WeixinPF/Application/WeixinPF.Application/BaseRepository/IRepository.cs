﻿using System;
using System.Collections.Generic;

namespace WeixinPF.Application.BaseRepository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        bool Add(TEntity entity);

        bool Update(TEntity entity);

        bool Update(IEnumerable<TEntity> entities);

        IList<TEntity> Get(Func<TEntity, bool> conditions);

        IList<TEntity> Get(int pageIndex, int pageSize, Func<TEntity, bool> conditions);
    }
}