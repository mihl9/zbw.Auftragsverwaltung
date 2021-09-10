using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.Common.Contracts;
using zbw.Auftragsverwaltung.Core.Invoices.Entities;

namespace zbw.Auftragsverwaltung.Core.Invoices.Contracts
{
    public interface IInvoiceRepository : IRepository<Invoice,Guid>
    {
    }
}
