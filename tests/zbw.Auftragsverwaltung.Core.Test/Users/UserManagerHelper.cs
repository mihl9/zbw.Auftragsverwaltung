using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Moq;
using zbw.Auftragsverwaltung.Core.Users.Entities;

namespace zbw.Auftragsverwaltung.Core.Test.Users
{
    public class UserManagerTestHelper
    {
        public static Mock<UserManager<TUser>> TestUserManager<TUser>(IList<TUser> users) where TUser : class
        {
            var store = new Mock<IUserPasswordStore<TUser>>();
            var manager = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            manager.Object.UserValidators.Add(new UserValidator<TUser>());
            manager.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            manager.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            manager.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => users.Add(x));
            manager.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            
            return manager;
        }
    }
}
