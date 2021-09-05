using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Domain.Users;

namespace zbw.Auftragsverwaltung.Client.Authentication
{
    public interface IAuthenticationClient
    {
        public Task<bool> Register(RegisterRequest request);

        public Task<AuthenticateResponse> Login(AuthenticateRequest request);

        public Task<bool> Logout();

        public Task<AuthenticateResponse> RefreshToken(RefreshTokenRequest request);

        public Task<bool> RevokeToken(RevokeTokenRequest request);
    }
}
