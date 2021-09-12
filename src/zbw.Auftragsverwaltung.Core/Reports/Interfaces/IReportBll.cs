using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Domain.ArticleGroups;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Reports;

namespace zbw.Auftragsverwaltung.Core.Reports.Interfaces
{
    public interface IReportBll
    {
        Task<IReadOnlyList<ArticleGroupDto>> GetCTERecursiveArticleGroup();

        Task<IReadOnlyList<FacturaDto>> GetFactura(string? customerNr, DateTime invoiceDate, int? invoiceNumber, string zipCode, string street, string name);
    }
}
