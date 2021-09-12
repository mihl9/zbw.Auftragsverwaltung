using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using zbw.Auftragsverwaltung.Core.Addresses.Contracts;
using zbw.Auftragsverwaltung.Core.Addresses.Entities;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Contracts;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Core.Invoices.Contracts;
using zbw.Auftragsverwaltung.Core.Invoices.Entities;

namespace zbw.Auftragsverwaltung.Core.Test.Invoices
{
    public class InvoiceRepositoryHelper
    {
        public static Mock<IInvoiceRepository> TestInvoiceRepository(IList<Invoice> invoice)
        {
            var repo = new Mock<IInvoiceRepository>();

            repo.Setup(x => x.DeleteAsync(It.IsAny<Invoice>())).ReturnsAsync(true).Callback<Invoice>(x => invoice.Remove(x));
            repo.Setup(x => x.UpdateAsync(It.IsAny<Invoice>())).ReturnsAsync(true);
            repo.Setup(x => x.AddAsync(It.IsAny<Invoice>())).ReturnsAsync((Invoice c) => c).Callback<Invoice>( invoice.Add);
            repo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => invoice.First(x => x.Id.Equals(id)));

            return repo;
        }
    }
}
