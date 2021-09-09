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
        Task<IReadOnlyList<ArticleGroupDto>> GetCTERecursiveArticleGroup();
    }
}
