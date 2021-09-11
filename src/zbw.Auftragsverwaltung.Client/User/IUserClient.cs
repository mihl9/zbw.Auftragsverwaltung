using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Users;

namespace zbw.Auftragsverwaltung.Client.User
{
    public interface IUserClient
    {
        public Task<UserDto> Get(Guid id);

        public Task<PaginatedList<UserDto>> List(int size = 10, int page = 1, bool deleted = false);

        public Task<UserDto> Add(UserDto customer);

        public Task<UserDto> Update(UserDto customer);

        public Task<bool> Delete(Guid id);
    }
}
