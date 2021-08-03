using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.Common.DTO;

namespace zbw.Auftragsverwaltung.Core.Common.Interfaces
{
    public interface ICrudAuthorizedBll<TDto, TEntity, in TKey, in TUserId> where TDto : class
    {
        public Task<TDto> Get(TKey id, TUserId userId);

        public Task<PaginatedList<TDto>> GetList(Expression<Func<TEntity, bool>> predicate, TUserId userId, int size = 10, int page = 1);

        public Task<PaginatedList<TDto>> GetList(TUserId userId, bool deleted = false, int size = 10, int page = 1);

        public Task<TDto> Add(TDto dto, TUserId userId);

        public Task<bool> Delete(TDto dto, TUserId userId);

        public Task<TDto> Update(TDto dto, TUserId userId);
    }
}
