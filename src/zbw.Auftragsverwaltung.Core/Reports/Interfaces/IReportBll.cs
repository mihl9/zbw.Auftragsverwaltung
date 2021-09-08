using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Domain.ArticleGroups;
using zbw.Auftragsverwaltung.Domain.Common;

namespace zbw.Auftragsverwaltung.Core.Reports.Interfaces
{
    public interface IReportBll
    {
        Task<PaginatedList<ArticleGroupDto>> GetCTERecursiveArticleGroup(int size = 10, int page = 1);
    }
}
