using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;
using zbw.Auftragsverwaltung.Core.Users.Dto;
using zbw.Auftragsverwaltung.Core.Users.Entities;

namespace zbw.Auftragsverwaltung.Core.Users.Interfaces
{
    public interface IUserBll : ICrudBll<UserDto, User, Guid>
    {
        public Task<IdentityResult> AddToRole(User user, string role);

        public Task<IdentityResult> RemoveFromRole(User user, string role);

        public Task<IdentityResult> ChangeUserPassword(User user, string currentPassword, string newPassword);
    }
}
