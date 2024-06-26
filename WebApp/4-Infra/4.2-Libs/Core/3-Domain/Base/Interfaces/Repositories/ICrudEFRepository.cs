﻿using System.Linq.Expressions;

namespace Core
{
    public interface ICrudEfRepository<T> : ICrudRepository<T> where T : IBaseAuditEntity
    {
        T GetById(long id, bool asNoTracking = true);

        T GetWhere(Expression<Func<T, bool>> expression);

        IQueryable<T> GetAllWhere(Expression<Func<T, bool>> expression);

        void Commit();
    }
}