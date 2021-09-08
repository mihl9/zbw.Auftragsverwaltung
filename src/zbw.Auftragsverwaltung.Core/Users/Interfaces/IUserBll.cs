using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Domain.Users;

namespace zbw.Auftragsverwaltung.Core.Users.Interfaces
{
    public interface IUserBll : ICrudBll<UserDto, User, Guid>
    {
        public Task<IdentityResult> AddToRole(User user, string role);

        public Task<IdentityResult> RemoveFromRole(User user, string role);

        public Task<IdentityResult> ChangeUserPassword(User user, string currentPassword, string newPassword);

        public Task<bool> Register(RegisterRequest request);

        public Task<AuthenticateResponse> Authenticate(AuthenticateRequest request, string ipAddress);

        public Task<bool> Logout();
        public Task<AuthenticateResponse> RefreshToken(object token, string ipAddress);

        public Task<bool> ValidateToken(object token);
        public Task<bool> RevokeToken(object token, string ipAddress);

    }
}
