using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using zbw.Auftragsverwaltung.Core.Common.Contracts;
using zbw.Auftragsverwaltung.Domain.Common;

namespace zbw.Auftragsverwaltung.Infrastructure.Common.Repositories
{
    public class BaseRepository<TI, TKey, TDbContext> : IRepository<TI, TKey> where TI : class where TDbContext : DbContext
    {

        protected readonly TDbContext _dbContext;

        public BaseRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<TI> GetByIdAsync(TKey id)
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

        public virtual async Task<bool> UpdateAsync(TI entity)
        {
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public virtual async Task<bool> DeleteAsync(TI entity)
        {
            try
            {
                _dbContext.Set<TI>().Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
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
