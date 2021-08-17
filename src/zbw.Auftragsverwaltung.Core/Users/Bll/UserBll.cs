using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using zbw.Auftragsverwaltung.Core.Customers.Interfaces;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Interfaces;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Customers;
using zbw.Auftragsverwaltung.Domain.Users;

namespace zbw.Auftragsverwaltung.Core.Users.Bll
{
    public class UserBll : IUserBll
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ICustomerBll _customer;
        private readonly IMapper _mapper;

        public UserBll(RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager, IMapper mapper, ICustomerBll customer)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            _customer = customer;
        }

        public async Task<UserDto> Get(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var result = _mapper.Map<UserDto>(user);
            result.AssignedCustomers = (await GetCustomersForUser(result)).ToList();
            return result;
        }

        public Task<PaginatedList<UserDto>> GetList(Expression<Func<User, bool>> predicate, int size = 10, int page = 1)
        {
            var users = PaginatedList<User>.ToPagedResult(_userManager.Users.Where(predicate).ToList(), page, size);
            return Task.FromResult(_mapper.Map<PaginatedList<UserDto>>(users));
        }

        public Task<PaginatedList<UserDto>> GetList(bool deleted = false, int size = 10, int page = 1)
        {
            var users = PaginatedList<User>.ToPagedResult(_userManager.Users.ToList(), page, size);
            return Task.FromResult(_mapper.Map<PaginatedList<UserDto>>(users));
        }

        public async Task<UserDto> Add(UserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            var result = await _userManager.CreateAsync(user, dto.Password);
            if (result.Succeeded)
            {
                return _mapper.Map<UserDto>(await _userManager.FindByNameAsync(user.UserName));
            }

            return new UserDto();
        }

        public async Task<bool> Delete(UserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            var result = await _userManager.DeleteAsync(user);
            var customers = await GetCustomersForUser(dto);
            foreach (var customer in customers)
            {
                await _customer.Delete(customer, dto.Id);
            }
            return result.Succeeded;
        }

        public async Task<UserDto> Update(UserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            var result = await _userManager.UpdateAsync(user);

            user = await _userManager.FindByIdAsync(user.Id.ToString());
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IdentityResult> AddToRole(User user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> RemoveFromRole(User user, string role)
        {
            return await _userManager.RemoveFromRoleAsync(user, role);
        }

        public async Task<IdentityResult> ChangeUserPassword(User user, string currentPassword, string newPassword)
        {
            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            return result;
        }

        private async Task<IEnumerable<CustomerDto>> GetCustomersForUser(UserDto user)
        {
            var result = await _customer.GetForUser(user, user.Id);
            return result;
        }
    }
}
