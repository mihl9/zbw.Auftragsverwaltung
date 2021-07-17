using System.Collections;
using System.Collections.Generic;
using System.Linq;
using zbw.Auftragsverwaltung.Core.Common.Dto;

namespace zbw.Auftragsverwaltung.Core.Common.DTO
{
    public class PaginatedList<T> : PagedResultBase where T : class
    {
		public List<T> Results { get; set; } = new List<T>();

        public PaginatedList(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize) : base(totalCount, pageNumber, pageSize)
        {

            Results.AddRange(items);
        }

        public PaginatedList() : base()
        { }

        public static PaginatedList<T> ToPagedResult(IEnumerable<T> source, int page, int size)
        {
            var count = source.Count();
            if (size != 0)
            {
                var items = source.Skip((page - 1) * size)
                    .Take(size).ToList();
                return new PaginatedList<T>(items, count, page, size);
            }
            else
            {
                return new PaginatedList<T>(source, count, 1, size);
            }

        }

		public T this[int index] => Results.ElementAt(index);
    }
}
