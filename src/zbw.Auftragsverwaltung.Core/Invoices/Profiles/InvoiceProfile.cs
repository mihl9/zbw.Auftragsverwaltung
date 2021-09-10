using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.Invoices.Entities;
using zbw.Auftragsverwaltung.Domain.Invoices;

namespace zbw.Auftragsverwaltung.Core.Invoices.Profiles
{
    public class InvoiceProfile : Profile
    {
        public InvoiceProfile()
        {
            CreateMap<Invoice, InvoiceDto>().ReverseMap();
        }
    }
}
