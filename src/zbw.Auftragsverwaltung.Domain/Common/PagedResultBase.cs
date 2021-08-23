using System;

namespace zbw.Auftragsverwaltung.Domain.Common
{
	public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public int FirstRowOnPage => (CurrentPage - 1) * PageSize + 1;

        public int LastRowOnPage => Math.Min(CurrentPage * PageSize, TotalCount);

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        protected PagedResultBase(int totalCount, int pageNumber, int pageSize)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }

        protected PagedResultBase()
        {
            TotalCount = 0;
            PageSize = 0;
            CurrentPage = 0;
            TotalPages = 0;
        }
    }

}
