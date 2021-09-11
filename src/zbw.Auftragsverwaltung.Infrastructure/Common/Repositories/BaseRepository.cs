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
    public class BaseRepository<TEntity, TKey, TDbContext> : IRepository<TEntity, TKey> where TEntity : class where TDbContext : DbContext
    {

        protected readonly TDbContext _dbContext;

        public BaseRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetByCompositeAsync(params object[] keys)
        {
            return await _dbContext.Set<TEntity>().FindAsync(keys);
        }

        public virtual async Task<IReadOnlyList<TEntity>> ListAsync(bool deleted = false)
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<IReadOnlyList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, bool deleted = false)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
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

        public virtual async Task<bool> DeleteAsync(TEntity entity)
        {
            try
            {
                _dbContext.Set<TEntity>().Remove(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public virtual async Task<PaginatedList<TEntity>> GetPagedResponseAsync(int page, int size, bool deleted = false)
        {
            return PaginatedList<TEntity>.ToPagedResult(await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync(), page, size);
        }

        public virtual async Task<PaginatedList<TEntity>> GetPagedResponseAsync(int page, int size, Expression<Func<TEntity, bool>> predicate, bool deleted = false)
        {
            return PaginatedList<TEntity>.ToPagedResult(
                await _dbContext.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync(), page, size);
        }
    }
}
