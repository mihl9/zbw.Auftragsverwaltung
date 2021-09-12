using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Contracts;
using zbw.Auftragsverwaltung.Core.Invoices.Contracts;
using zbw.Auftragsverwaltung.Core.Reports.Interfaces;
using zbw.Auftragsverwaltung.Domain.ArticleGroups;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Reports;

namespace zbw.Auftragsverwaltung.Core.Reports.BLL
{
    public class ReportBll : IReportBll
    {
        private readonly IArticleGroupRepository _articleGroupRepository;
        private readonly IMapper _mapper;
        private readonly IInvoiceRepository _invoiceRepository;

        public ReportBll(IArticleGroupRepository articleGroupRepository, IMapper mapper, IInvoiceRepository invoiceRepository)
        {
            _articleGroupRepository = articleGroupRepository;
            _mapper = mapper;
            _invoiceRepository = invoiceRepository;
        }

        public async Task<IReadOnlyList<ArticleGroupDto>> GetCTERecursiveArticleGroup()
        {
            var cteArticleGroups = await _articleGroupRepository.GetCTERecursive();
            return _mapper.Map<IReadOnlyList<ArticleGroupDto>>(cteArticleGroups);

        }

        public async Task<IReadOnlyList<FacturaDto>> GetFactura(int? customerNr, DateTime invoiceDate, int? invoiceNumber,
            string zipCode, string street, string name)
        {
            var facturas = await _invoiceRepository.GetFactura(customerNr, invoiceDate, invoiceNumber, zipCode, street, name);
            return _mapper.Map<IReadOnlyList<FacturaDto>>(facturas);
        }
    }
}
