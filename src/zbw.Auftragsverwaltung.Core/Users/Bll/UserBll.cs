using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using zbw.Auftragsverwaltung.Core.Common.Exceptions;
using zbw.Auftragsverwaltung.Core.Common.Helpers;
using zbw.Auftragsverwaltung.Core.Customers.Interfaces;
using zbw.Auftragsverwaltung.Core.Users.Contracts;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Core.Users.Interfaces;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Customers;
using zbw.Auftragsverwaltung.Domain.Users;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Enumerations;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Models;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Exceptions;

namespace zbw.Auftragsverwaltung.Core.Users.Bll
{
    public class UserBll : IUserBll
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ICustomerBll _customer;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserRepository _userRepository;

        public UserBll(RoleManager<IdentityRole<Guid>> roleManager, UserManager<User> userManager, IMapper mapper, ICustomerBll customer, ITokenService tokenService, SignInManager<User> signInManager, IUserRepository userRepository)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
            _customer = customer;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userRepository = userRepository;
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

        public async Task<bool> Register(RegisterRequest request)
        {
            var user = new User() { UserName = request.UserName, Email = request.Email };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var states = new ModelStateDictionary();
                foreach (var identityError in result.Errors)
                {
                    states.AddModelError(identityError.Code, identityError.Description);
                }

                throw new HttpRegistrationFailedException("Registration Failed", states: states);
            }

            await _userManager.AddToRoleAsync(user, Roles.User.ToString());
            return true;
        }

        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request, string ipAddress)
        {
            var user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
                throw new UserNotFoundException();


            var signinResult = await _signInManager.PasswordSignInAsync(user, request.Password, false, true);

            if (signinResult.Succeeded)
            {
                var token = await _tokenService.GenerateAuthenticationToken(user);
                var refreshToken = await _tokenService.GenerateRefreshToken(ipAddress);
                
                user.RefreshTokens.Clear();
                user.RefreshTokens.Add(refreshToken);
                await _userRepository.UpdateAsync(user);
                return new AuthenticateResponse() { AuthenticationToken = token, RefreshToken = refreshToken};
            }
            if (signinResult.IsNotAllowed)
            {
                if (!user.EmailConfirmed)
                {
                    throw new HttpUnauthorizedException("Access Denied", "Email is not Confirmed");
                }
                throw new HttpUnauthorizedException("Access Denied", signinResult.ToString());
            }

            if (signinResult.RequiresTwoFactor)
                throw new HttpUnauthorizedException("Access Denied", "Requires Two Factor");

            throw new HttpUnauthorizedException("Access Denied");
        }

        public async Task<bool> Logout()
        {
            await _signInManager.SignOutAsync();
            return true;
        }

        public async Task<AuthenticateResponse> RefreshToken(object token, string ipAddress)
        {
            var user = await _userRepository.GetUserByRefreshToken(token);

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token.ToString());

            if (!refreshToken.IsActive) throw new HttpUnauthorizedException("Invalid Token");

            var newRefreshToken = await _tokenService.GenerateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);

            await _userRepository.UpdateAsync(user);

            var jwtToken = await _tokenService.GenerateAuthenticationToken(user);

            return new AuthenticateResponse() { AuthenticationToken = jwtToken, RefreshToken = newRefreshToken };
        }

        public async Task<bool> RevokeToken(object token, string ipAddress)
        {
            var user = await _userRepository.GetUserByRefreshToken(token);

            if (user == null) return false;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token.ToString());

            if (!refreshToken.IsActive) return false;

            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.ReplacedByToken = ipAddress;

            await _userRepository.UpdateAsync(user);

            return true;
        }

        private async Task<IEnumerable<CustomerDto>> GetCustomersForUser(UserDto user)
        {
            var result = await _customer.GetForUser(user.Id, user.Id);
            return result;
        }


    }
}
