using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.Common.DTO;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;

namespace zbw.Auftragsverwaltung.Core.Common.Contracts
{
    public interface IRepository<TI, TKey> where TI : class
    {
        Task<TI> GetByIdAsync(TKey id);
        Task<IReadOnlyList<TI>> ListAsync();
        Task<IReadOnlyList<TI>> ListAsync(Expression<Func<TI, bool>> predicate);
        Task<TI> AddAsync(TI entity);
        Task UpdateAsync(TI entity);
        Task DeleteAsync(TI entity);
        Task<PaginatedList<TI>> GetPagedResponseAsync(int page, int size);
        Task<PaginatedList<TI>> GetPagedResponseAsync(int page, int size, Expression<Func<TI, bool>> predicate);
    }
}
