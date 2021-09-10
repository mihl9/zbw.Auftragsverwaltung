using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using zbw.Auftragsverwaltung.Core.Addresses.Contracts;
using zbw.Auftragsverwaltung.Core.Addresses.Entities;
using zbw.Auftragsverwaltung.Core.Addresses.Interfaces;
using zbw.Auftragsverwaltung.Core.Common.Exceptions;
using zbw.Auftragsverwaltung.Core.Common.Helpers;
using zbw.Auftragsverwaltung.Core.Customers.BLL;
using zbw.Auftragsverwaltung.Core.Customers.Interfaces;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Domain.Addresses;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Customers;

namespace zbw.Auftragsverwaltung.Core.Addresses.BLL
{
    public class AddressBll : IAddressBll
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly ICustomerBll _customerBll;

        public AddressBll(IAddressRepository addressRepository, IMapper mapper, UserManager<User> userManager, ICustomerBll customerBll)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
            _userManager = userManager;
            _customerBll = customerBll;
        }
        
        public async Task<AddressDto> Get(Guid id, Guid userId)
        {
            var user = await _userManager.GetUser(userId);

            var address = await _addressRepository.GetByIdAsync(id);
            var customer = await _customerBll.Get(address.CustomerId, userId);
            var dto = _mapper.Map<AddressDto>(address);
            CalculateFields(dto);
            return dto;
        }

        public async Task<PaginatedList<AddressDto>> GetList(Expression<Func<Address, bool>> predicate, Guid userId, int size = 10, int page = 1)
        {
            var user = await _userManager.GetUser(userId);

            if (!await _userManager.IsInRoleAsync(user, Roles.Administrator.ToString()))
            {
                var customers = await _customerBll.GetForUser(userId, user.Id);
                predicate = predicate.And(x => customers.Any(y => y.Id.Equals(x.CustomerId)));
            }

            var addresses = await _addressRepository.GetPagedResponseAsync(page, size, predicate);

            return _mapper.Map<PaginatedList<AddressDto>>(addresses);
        }

        public async Task<PaginatedList<AddressDto>> GetList(Guid userId, bool deleted = false, int size = 10, int page = 1)
        {
            return await GetList(PredicateBuilder.True<Address>(), userId, size, page);
        }

        public async Task<AddressDto> Add(AddressDto dto, Guid userId)
        {
            var user = await _userManager.GetUser(userId);
            var customer = await _customerBll.Get(dto.CustomerId, userId);
            var address = _mapper.Map<Address>(dto);
            address = await _addressRepository.AddAsync(address);

            return _mapper.Map<AddressDto>(address);
        }

        public async Task<bool> Delete(AddressDto dto, Guid userId)
        {
            var user = await _userManager.GetUser(userId);
            var customer = await _customerBll.Get(dto.CustomerId, userId);
            var address = _mapper.Map<Address>(dto);

            return await _addressRepository.DeleteAsync(address);
        }

        public async Task<AddressDto> Update(AddressDto dto, Guid userId)
        {
            var user = await _userManager.GetUser(userId);
            var customer = await _customerBll.Get(dto.CustomerId, userId);

            var address = _mapper.Map<Address>(dto);

            await _addressRepository.UpdateAsync(address);

            //address = await _addressRepository.GetByCompositeAsync(address.Id);

            return _mapper.Map<AddressDto>(address);
        }

        private void CalculateFields(AddressDto address)
        {
            CalculateFullAddress(address);
        }

        private void CalculateFullAddress(AddressDto address)
        {
            address.FullAddress = $"{address.Street} {address.Number}, {address.Zip} {address.Location}";
        }
    }
}
