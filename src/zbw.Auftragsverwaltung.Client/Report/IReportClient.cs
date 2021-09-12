using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Domain.ArticleGroups;
using zbw.Auftragsverwaltung.Domain.Reports;

namespace zbw.Auftragsverwaltung.Client.Report
{
    public interface IReportClient
    {
       public Task<IReadOnlyList<ArticleGroupDto>> GetCTEList();

       public Task<IReadOnlyList<FacturaDto>> GetFactura(int? customerNr, DateTime invoiceDate, int? invoiceNumber, string zipCode, string street, string name);
    
    }
}
