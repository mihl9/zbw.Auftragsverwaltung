using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Domain.Common;

namespace zbw.Auftragsverwaltung.Core.Common.Contracts
{
    public interface IRepository<TI, TKey> where TI : class
    {
        Task<TI> GetByIdAsync(TKey id);

        Task<TI> GetByCompositeAsync(params object[] keys);

        Task<IReadOnlyList<TI>> ListAsync();
        Task<IReadOnlyList<TI>> ListAsync(Expression<Func<TI, bool>> predicate);
        Task<TI> AddAsync(TI entity);
        Task<bool> UpdateAsync(TI entity);
        Task<bool> DeleteAsync(TI entity);
        Task<PaginatedList<TI>> GetPagedResponseAsync(int page, int size);
        Task<PaginatedList<TI>> GetPagedResponseAsync(int page, int size, Expression<Func<TI, bool>> predicate);
    }
}
