using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using zbw.Auftragsverwaltung.Core.Common.Exceptions;
using zbw.Auftragsverwaltung.Core.Users.Entities;

namespace zbw.Auftragsverwaltung.Core.Common.Helpers
{
    public static class UserManagerExtensions
    {
        public static async Task<User> GetUser(this UserManager<User> userManager, Guid userId, bool throwIfNotFound = true)
        {
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user != null) return user;

            if (throwIfNotFound)
                throw new UserNotFoundException();

            return new User();
        }
    }
}
