using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using zbw.Auftragsverwaltung.Core.Common.Contracts;
using zbw.Auftragsverwaltung.Core.Common.DTO;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;

namespace zbw.Auftragsverwaltung.Infrastructure.Common.Repositories
{
    public class BaseRepository<TI> : IRepository<TI> where TI : class, IEntity
    {

        protected readonly OrderManagementContext _dbContext;

        public BaseRepository(OrderManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<TI> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<TI>().FindAsync(id);
        }

        public virtual async Task<IReadOnlyList<TI>> ListAsync()
        {
            return await _dbContext.Set<TI>().ToListAsync();
        }

        public virtual async Task<IReadOnlyList<TI>> ListAsync(Expression<Func<TI, bool>> predicate)
        {
            return await _dbContext.Set<TI>().Where(predicate).ToListAsync();
        }

        public virtual async Task<TI> AddAsync(TI entity)
        {
            await _dbContext.Set<TI>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task UpdateAsync(TI entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TI entity)
        {
            _dbContext.Set<TI>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<PaginatedList<TI>> GetPagedResponseAsync(int page, int size)
        {
            return PaginatedList<TI>.ToPagedResult(await _dbContext.Set<TI>().AsNoTracking().ToListAsync(), page, size);
        }

        public virtual async Task<PaginatedList<TI>> GetPagedResponseAsync(int page, int size, Expression<Func<TI, bool>> predicate)
        {
            return PaginatedList<TI>.ToPagedResult(
                await _dbContext.Set<TI>().Where(predicate).AsNoTracking().ToListAsync(), page, size);
        }
    }
}
