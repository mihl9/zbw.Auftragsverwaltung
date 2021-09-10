using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;
using zbw.Auftragsverwaltung.Core.Invoices.Entities;
using zbw.Auftragsverwaltung.Domain.Invoices;

namespace zbw.Auftragsverwaltung.Core.Invoices.Interfaces
{
    public interface IInvoiceBll : ICrudBll<InvoiceDto,Invoice,Guid>
    {
    }
}
