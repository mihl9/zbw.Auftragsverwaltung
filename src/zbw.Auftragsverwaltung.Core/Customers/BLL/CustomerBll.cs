using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using zbw.Auftragsverwaltung.Core.Common.Contracts;
using zbw.Auftragsverwaltung.Core.Customers.Dto;
using zbw.Auftragsverwaltung.Core.Customers.Entities;
using zbw.Auftragsverwaltung.Core.Customers.Interfaces;

namespace zbw.Auftragsverwaltung.Core.Customers.BLL
{
    public class CustomerBll : ICustomerBll
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IMapper _mapper;

        public CustomerBll(IRepository<Customer> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Get(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            return _mapper.Map<CustomerDto>(customer);
        }
    }
}
