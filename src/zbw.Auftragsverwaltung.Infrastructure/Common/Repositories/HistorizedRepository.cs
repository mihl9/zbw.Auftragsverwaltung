using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using zbw.Auftragsverwaltung.Core.Common.Contracts;
using zbw.Auftragsverwaltung.Core.Common.Helpers;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;
using zbw.Auftragsverwaltung.Domain.Common;

namespace zbw.Auftragsverwaltung.Infrastructure.Common.Repositories
{
    public class HistorizedRepository<TEntity, TKey, TDbContext> :BaseRepository<TEntity, TKey, TDbContext>, IHistorizedRepository<TEntity, TKey> where TEntity : class, IEntityHistorized where TDbContext : DbContext
    {
        public HistorizedRepository(TDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<TEntity> GetByIdAsync(TKey id)
        {
            var entities = _dbContext.Set<TEntity>().Where(x => x.Id.Equals(id)).AsNoTracking();

            if (!await entities.AnyAsync())
                return null;

            var entity = entities.First(x => x.ValidTo == null);

            if (entity != null)
                return entity;

            return await entities.OrderBy(x => x.ValidTo).FirstAsync();
        }

        

        public override async Task<PaginatedList<TEntity>> GetPagedResponseAsync(int page, int size, Expression<Func<TEntity, bool>> predicate, bool deleted = false)
        {
            if (!deleted)
                predicate = predicate.And(x => x.ValidTo == null);

            return PaginatedList<TEntity>.ToPagedResult(
                await _dbContext.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync(), page, size);
        }

        public override async Task<PaginatedList<TEntity>> GetPagedResponseAsync(int page, int size, bool deleted = false)
        {
            var predicate = PredicateBuilder.True<TEntity>();

            if (!deleted)
                predicate = predicate.And(x => x.ValidTo == null);

            return PaginatedList<TEntity>.ToPagedResult(
                await _dbContext.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync(), page, size);
        }

        public override async Task<IReadOnlyList<TEntity>> ListAsync(bool deleted = false)
        {
            var predicate = PredicateBuilder.True<TEntity>();

            if (!deleted)
                predicate = predicate.And(x => x.ValidTo == null);

            return await _dbContext.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync();
        }

        public override async Task<IReadOnlyList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate, bool deleted = false)
        {
            if (!deleted)
                predicate = predicate.And(x => x.ValidTo == null);

            return await _dbContext.Set<TEntity>().Where(predicate).AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetSpecificEntity(TKey id, DateTime @from)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id, @from);
        }

        public override async Task<bool> UpdateAsync(TEntity entity)
        {
            if (entity.ValidFrom == DateTime.MinValue)
            {
                var oldEntity = await this.GetByIdAsync((TKey)TypeDescriptor.GetConverter(typeof(TKey)).ConvertFromInvariantString(entity.Id.ToString()));
                entity.ValidFrom = oldEntity.ValidFrom;
            }
            return await base.UpdateAsync(entity);
        }
    }
}
