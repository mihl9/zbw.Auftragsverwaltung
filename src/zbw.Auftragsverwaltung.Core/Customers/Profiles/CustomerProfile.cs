using AutoMapper;
using zbw.Auftragsverwaltung.Core.Customers.Entities;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Customers;

namespace zbw.Auftragsverwaltung.Core.Customers.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<PaginatedList<Customer>, PaginatedList<CustomerDto>>();
        }
    }
}
