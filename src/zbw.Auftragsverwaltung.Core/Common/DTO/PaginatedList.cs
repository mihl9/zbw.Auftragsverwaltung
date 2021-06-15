using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace zbw.Auftragsverwaltung.Core.Common.DTO
{
    public class PaginatedList<TI> : IReadOnlyList<TI>
    {
        private readonly IEnumerable<TI> _enumerable;
        public int CurrentPage { get; }
        public int PageSize { get; }
        public int MaxElements { get; }

        public PaginatedList(IEnumerable<TI> enumerator, int pageSize, int currentPage, int maxElements)
        {
            _enumerable = enumerator;
            PageSize = pageSize;
            CurrentPage = currentPage;
            MaxElements = maxElements;
        }

        public IEnumerator<TI> GetEnumerator()
        {
            return _enumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _enumerable.Count();
        
        public TI this[int index] => _enumerable.ElementAt(index);
    }
}
