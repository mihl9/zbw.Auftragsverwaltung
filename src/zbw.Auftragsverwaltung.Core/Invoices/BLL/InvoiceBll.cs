using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using zbw.Auftragsverwaltung.Core.Invoices.Contracts;
using zbw.Auftragsverwaltung.Core.Invoices.Entities;
using zbw.Auftragsverwaltung.Core.Invoices.Interfaces;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Invoices;

namespace zbw.Auftragsverwaltung.Core.Invoices.BLL
{
    public class InvoiceBll : IInvoiceBll
    {
        private readonly IMapper _mapper;
        private readonly IInvoiceRepository _invoiceRepository;

        public InvoiceBll(IMapper mapper, IInvoiceRepository invoiceRepository)
        {
            _mapper = mapper;
            _invoiceRepository = invoiceRepository;
        }

        public async Task<InvoiceDto> Get(Guid id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            return _mapper.Map<InvoiceDto>(invoice);

        }

        public async Task<PaginatedList<InvoiceDto>> GetList(Expression<Func<Invoice, bool>> predicate, int size = 10, int page = 1)
        {
            var invoices = await _invoiceRepository.GetPagedResponseAsync(page, size, predicate);
            return _mapper.Map<PaginatedList<InvoiceDto>>(invoices);
        }

        public async Task<PaginatedList<InvoiceDto>> GetList(bool deleted = false, int size = 10, int page = 1)
        {
            var invoices = await _invoiceRepository.GetPagedResponseAsync(page, size);
            return _mapper.Map<PaginatedList<InvoiceDto>>(invoices);
        }

        public async Task<InvoiceDto> Add(InvoiceDto dto)
        {
            dto.Id = new Guid();
            var invoice = _mapper.Map<Invoice>(dto);
            invoice =  await _invoiceRepository.AddAsync(invoice);

            return _mapper.Map<InvoiceDto>(invoice);
        }

        public async Task<bool> Delete(InvoiceDto dto)
        {
            var invoice = _mapper.Map<Invoice>(dto);
            return await _invoiceRepository.UpdateAsync(invoice);
        }

        public async Task<InvoiceDto> Update(InvoiceDto dto)
        {
            var invoice = _mapper.Map<Invoice>(dto);
            await _invoiceRepository.UpdateAsync(invoice);

            return _mapper.Map<InvoiceDto>(invoice);
        }
    }
}
