using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using zbw.Auftragsverwaltung.Core.Common.Exceptions;
using zbw.Auftragsverwaltung.Core.Common.Helpers;
using zbw.Auftragsverwaltung.Core.Customers.Contracts;
using zbw.Auftragsverwaltung.Core.Customers.Entities;
using zbw.Auftragsverwaltung.Core.Customers.Interfaces;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Customers;
using zbw.Auftragsverwaltung.Domain.Users;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Exceptions;

namespace zbw.Auftragsverwaltung.Core.Customers.BLL
{
    public class CustomerBll : ICustomerBll
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public CustomerBll(ICustomerRepository customerRepository, IMapper mapper, UserManager<User> userManager)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<CustomerDto> Get(Guid id, Guid userId)
        {
            var user = await GetUser(userId);

            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                throw new NotFoundByIdException($"Customer Id: {id} Not Found!");

            if (!await _userManager.IsInRoleAsync(user, Roles.Administrator.ToString()))
            {
                if (!customer.UserId.Equals(userId))
                {
                    throw new InvalidRightsException(user);
                }
            }
            
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<PaginatedList<CustomerDto>> GetList(Expression<Func<Customer, bool>> predicate, Guid userId, int size = 10, int page = 1)
        {
            var user = await GetUser(userId);

            if (!await _userManager.IsInRoleAsync(user, Roles.Administrator.ToString()))
            {
                predicate = predicate.And(x => x.UserId.Equals(userId));
            }
            
            var customers = await _customerRepository.GetPagedResponseAsync(page, size, predicate);

            return _mapper.Map<PaginatedList<CustomerDto>>(customers);
        }

        public async Task<PaginatedList<CustomerDto>> GetList(Guid userId, bool deleted = false, int size = 10, int page = 1)
        {
            var user = await GetUser(userId);

            var predicate = PredicateBuilder.True<Customer>();
            
            if (!await _userManager.IsInRoleAsync(user, Roles.Administrator.ToString()))
            {
                predicate = predicate.And(x => x.UserId.Equals(userId));
            }
            
            var customers = await _customerRepository.GetPagedResponseAsync(page, size, predicate);

            return _mapper.Map<PaginatedList<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> Add(CustomerDto dto, Guid userId)
        {
            var user = await GetUser(userId);

            dto.UserId = user.Id.ToString();
            dto.Id = new Guid();
            var customer = _mapper.Map<Customer>(dto);
            customer = await _customerRepository.AddAsync(customer);

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<bool> Delete(CustomerDto dto, Guid userId)
        {
            var user = await GetUser(userId);
            var customer = _mapper.Map<Customer>(dto);
            
            if (!await _userManager.IsInRoleAsync(user, Roles.Administrator.ToString()))
            {
                if (!customer.UserId.Equals(userId))
                    throw new InvalidRightsException(user);
            }

            return await _customerRepository.DeleteAsync(customer);
        }

        public async Task<CustomerDto> Update(CustomerDto dto, Guid userId)
        {
            var user = await GetUser(userId);
            var customer = _mapper.Map<Customer>(dto);

            if (!await _userManager.IsInRoleAsync(user, Roles.Administrator.ToString()))
            {
                if (!customer.UserId.Equals(userId))
                    throw new InvalidRightsException(user);
            }

            await _customerRepository.UpdateAsync(customer);

            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<IEnumerable<CustomerDto>> GetForUser(Guid id, Guid userId)
        {
            return (await GetList(x => x.UserId.Equals(id), userId, 0, 1)).Results;
        }

        private async Task<User> GetUser(Guid userId, bool throwIfNotFound = true)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user != null) return user;

            if (throwIfNotFound)
                throw new UserNotFoundException();
            
            return new User();
        }
    }
}
