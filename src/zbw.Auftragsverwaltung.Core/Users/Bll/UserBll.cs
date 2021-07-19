using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using zbw.Auftragsverwaltung.Core.Common.DTO;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;
using zbw.Auftragsverwaltung.Core.Users.Dto;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Interfaces;

namespace zbw.Auftragsverwaltung.Core.Users.Bll
{
    public class UserBll : IUserBll
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IMapper _mapper;

        public UserBll(RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserDto> Get(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return _mapper.Map<UserDto>(user);
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

            return null;
        }

        public async Task<bool> Delete(UserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }

        public async Task<UserDto> Update(UserDto dto)
        {
            var user = _mapper.Map<User>(dto);
            var result = await _userManager.UpdateAsync(user);

            user = await _userManager.FindByIdAsync(user.Id.ToString());
            return _mapper.Map<UserDto>(user);
        }
    }
}
