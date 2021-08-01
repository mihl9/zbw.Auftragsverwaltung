using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using zbw.Auftragsverwaltung.Core.Common.Contracts;
using zbw.Auftragsverwaltung.Core.Common.DTO;
using zbw.Auftragsverwaltung.Core.Customers.Contracts;
using zbw.Auftragsverwaltung.Core.Customers.Dto;
using zbw.Auftragsverwaltung.Core.Customers.Entities;
using zbw.Auftragsverwaltung.Core.Customers.Interfaces;
using zbw.Auftragsverwaltung.Core.Users.Entities;

namespace zbw.Auftragsverwaltung.Core.Customers.BLL
{
    public class CustomerBll : ICustomerBll
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerBll(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerDto> Get(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<PaginatedList<CustomerDto>> GetList(Expression<Func<Customer, bool>> predicate, int size = 10, int page = 1)
        {
            var customers = await _customerRepository.GetPagedResponseAsync(page, size, predicate);

            return _mapper.Map<PaginatedList<CustomerDto>>(customers);
        }

        public async Task<PaginatedList<CustomerDto>> GetList(bool deleted = false, int size = 10, int page = 1)
        {
            var customers = await _customerRepository.GetPagedResponseAsync(page, size);

            return _mapper.Map<PaginatedList<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> Add(CustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            customer = await _customerRepository.AddAsync(customer);

            return _mapper.Map<CustomerDto>(customer);
        }

        public Task<bool> Delete(CustomerDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerDto> Update(CustomerDto dto)
        {
            var customer = _mapper.Map<Customer>(dto);
            await _customerRepository.UpdateAsync(customer);

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<IEnumerable<CustomerDto>> GetForUser(User user)
        {
            return (await GetList(x => x.UserId == user.Id, 0, 1)).Results;
        }
    }
}
