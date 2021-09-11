using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Domain.Common;

namespace zbw.Auftragsverwaltung.Core.Common.Contracts
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(TKey id);

        Task<TEntity> GetByCompositeAsync(params object[] keys);

        Task<IReadOnlyList<TEntity>> ListAsync(bool deleted = false);
        Task<IReadOnlyList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, bool deleted = false);
        Task<TEntity> AddAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<PaginatedList<TEntity>> GetPagedResponseAsync(int page, int size, bool deleted = false);
        Task<PaginatedList<TEntity>> GetPagedResponseAsync(int page, int size, Expression<Func<TEntity, bool>> predicate, bool deleted = false);
    }
}
