using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;

namespace zbw.Auftragsverwaltung.Core.Common.Contracts
{
    public interface IHistorizedRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntityHistorized
    {
        public Task<TEntity> GetSpecificEntity(TKey id, DateTime from);
    }
}
