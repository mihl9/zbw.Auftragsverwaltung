using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.Users.Entities;

namespace zbw.Auftragsverwaltung.Core.Users.Contracts
{
    public interface ITokenService
    {
        public Task<object> GenerateAuthenticationToken(User user);

        public Task<RefreshToken> GenerateRefreshToken(string ipAddress);
    }
}
