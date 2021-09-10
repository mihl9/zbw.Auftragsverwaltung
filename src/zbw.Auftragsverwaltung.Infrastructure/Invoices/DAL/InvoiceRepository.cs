using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.Invoices.Contracts;
using zbw.Auftragsverwaltung.Core.Invoices.Entities;
using zbw.Auftragsverwaltung.Infrastructure.Common.Repositories;

namespace zbw.Auftragsverwaltung.Infrastructure.Invoices.DAL
{
    public class InvoiceRepository : BaseRepository<Invoice, Guid, OrderManagementContext>, IInvoiceRepository
    {
        public InvoiceRepository(OrderManagementContext dbContext) : base(dbContext)
        {
        }
    }
}
