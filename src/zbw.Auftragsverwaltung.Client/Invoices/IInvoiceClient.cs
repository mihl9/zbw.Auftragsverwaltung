using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Invoices;

namespace zbw.Auftragsverwaltung.Client.Invoices
{
    public interface IInvoiceClient
    {
        public Task<InvoiceDto> Get(Guid id);

        public Task<PaginatedList<InvoiceDto>> List(int size = 10, int page = 1, bool deleted = false);

        public Task<InvoiceDto> Add(InvoiceDto invoice);

        public Task<InvoiceDto> Update(InvoiceDto invoice);

        public Task<bool> Delete(Guid id);
    }
}
