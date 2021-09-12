using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.Test.Helpers;
using zbw.Auftragsverwaltung.Core.Users.Contracts;
using zbw.Auftragsverwaltung.Core.Users.Entities;

namespace zbw.Auftragsverwaltung.Core.Test.DataTransfer
{
    public class UserRepositoryHelper
    {
        public static Mock<IUserRepository> TestUserRepository(IList<User> users)
        {
            var repo = new Mock<IUserRepository>();
            repo.Setup(x => x.DeleteAsync(It.IsAny<User>())).ReturnsAsync(true).Callback<User>(x => users.Remove(x));
            repo.Setup(x => x.UpdateAsync(It.IsAny<User>())).ReturnsAsync(true);
            repo.Setup(x => x.AddAsync(It.IsAny<User>())).ReturnsAsync((User c) => c).Callback<User>(users.Add);
            repo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => users.First(x => x.Id.Equals(id)));
            repo.Setup(x => x.ListAsync(false)).Returns(GetListAsync(users));

            return repo;
        }

        private static async Task<IReadOnlyList<User>> GetListAsync(IList<User> users)
        {
            List<User> userList = new List<User>(users);
            IReadOnlyList<User> list = userList.AsReadOnly();
            IReadOnlyList<User> task = await Task.Run(() => list);
            return task;
        }
    }
}
