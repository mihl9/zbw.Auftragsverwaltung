using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.Common.Contracts;
using zbw.Auftragsverwaltung.Core.Invoices.Entities;
using zbw.Auftragsverwaltung.Domain.Reports;

namespace zbw.Auftragsverwaltung.Core.Invoices.Contracts
{
    public interface IInvoiceRepository : IRepository<Invoice,Guid>
    {
        Task<IReadOnlyList<FacturaDto>> GetFactura(string? customerNr, DateTime invoiceDate, int? invoiceNumber,
            string zipCode, string street, string name);
    }
}
