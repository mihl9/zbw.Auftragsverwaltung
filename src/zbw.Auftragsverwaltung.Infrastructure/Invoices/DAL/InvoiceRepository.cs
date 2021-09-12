using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Core.Invoices.Contracts;
using zbw.Auftragsverwaltung.Core.Invoices.Entities;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Reports;
using zbw.Auftragsverwaltung.Infrastructure.Addresses.DAL;
using zbw.Auftragsverwaltung.Infrastructure.Common.Repositories;

namespace zbw.Auftragsverwaltung.Infrastructure.Invoices.DAL
{
    public class InvoiceRepository : BaseRepository<Invoice, Guid, OrderManagementContext>, IInvoiceRepository
    {
        public InvoiceRepository(OrderManagementContext dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<FacturaDto>> GetFactura(int? customerNr, DateTime invoiceDate, int? invoiceNumber, string zipCode, string street, string name)
        {
            var query = await _dbContext.Invoices
                .Join(_dbContext.Addresses, i => i.AddressId, adr => adr.Id, (i, adr) => new { i, adr })
                .Join(_dbContext.Customers, @t => @t.adr.CustomerId, c => c.Id, (@t, c) => new { @t, c })
                .Where(@t => customerNr == null || @t.c.CustomerNr == customerNr)
                .Where(@t => invoiceDate == null || @t.@t.i.Date == invoiceDate)
                .Where(@t => invoiceNumber == null || @t.@t.i.Number == invoiceNumber)
                .Where(@t => zipCode == null || @t.@t.adr.Zip == zipCode)
                .Where(@t => street == null || @t.@t.adr.Street == street)
                .Where(@t => name == null ||@t.@t.adr.Recipient == name)
                .Select(@t => new FacturaDto()
                {
                   CustomerNr = @t.c.CustomerNr.ToString(),
                   Name = @t.@t.adr.Recipient,
                   Street = @t.@t.adr.Street,
                   Zip = @t.@t.adr.Zip,
                   Location = @t.@t.adr.Location,
                   InvoiceDate = @t.@t.i.Date,
                   InvoiceNumber = @t.@t.i.Number.ToString(),
                   Netto = @t.@t.i.Netto,
                   Brutto = @t.@t.i.Brutto
                }).ToListAsync();

            
            
            return query;



        }
    }
}
