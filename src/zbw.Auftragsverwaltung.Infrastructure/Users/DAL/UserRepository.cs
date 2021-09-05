using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<User> GetUserByRefreshToken(object token)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u =>
                u.RefreshTokens.Any(t => t.Token == token.ToString()));

            return user;
        }
    }
}
