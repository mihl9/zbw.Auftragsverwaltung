using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.Common.DTO;

namespace zbw.Auftragsverwaltung.Core.Common.Interfaces
{
    public interface ICrudBll<TDto, TEntity, in TKey> where TDto : class
    {
        public Task<TDto> Get(TKey id);

        public Task<PaginatedList<TDto>> GetList(Expression<Func<TEntity, bool>> predicate, int size = 10, int page = 1);

        public Task<PaginatedList<TDto>> GetList(bool deleted = false, int size = 10, int page = 1);

        public Task<TDto> Add(TDto dto);

        public Task<bool> Delete(TDto dto);

        public Task<TDto> Update(TDto dto);
    }
}
