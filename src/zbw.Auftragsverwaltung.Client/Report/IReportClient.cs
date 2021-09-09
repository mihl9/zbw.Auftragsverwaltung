using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Domain.ArticleGroups;

namespace zbw.Auftragsverwaltung.Client.Report
{
    public interface IReportClient
    {
       public Task<IReadOnlyList<ArticleGroupDto>> GetCTEList();
    
    }
}
