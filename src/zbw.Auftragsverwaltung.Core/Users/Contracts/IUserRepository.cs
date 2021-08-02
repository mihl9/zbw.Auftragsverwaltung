using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.Common.Contracts;
using zbw.Auftragsverwaltung.Core.Users.Entities;

namespace zbw.Auftragsverwaltung.Core.Users.Contracts
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        public Task<bool> AddToRole(User user, string role);

        public Task<bool> RemoveFromRole(User user, string role);

        public Task<bool> SetUserPassword(User user, string password);

    }
}
