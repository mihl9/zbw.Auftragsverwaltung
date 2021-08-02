using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.Common.Contracts;
using zbw.Auftragsverwaltung.Core.Common.DTO;
using zbw.Auftragsverwaltung.Core.Users.Contracts;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Infrastructure.Common.Repositories;

namespace zbw.Auftragsverwaltung.Infrastructure.Users.DAL
{
    public class UserRepository : BaseRepository<User, Guid, UserIdentityContext>, IUserRepository
    {
        public UserRepository(UserIdentityContext dbContext) : base(dbContext)
        {
        }

        public Task<bool> AddToRole(User user, string role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveFromRole(User user, string role)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetUserPassword(User user, string password)
        {
            throw new NotImplementedException();
        }
    }
}
