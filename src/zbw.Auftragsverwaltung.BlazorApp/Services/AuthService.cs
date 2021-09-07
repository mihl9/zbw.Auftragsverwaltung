using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using zbw.Auftragsverwaltung.Client;
using zbw.Auftragsverwaltung.Client.Authentication;
using zbw.Auftragsverwaltung.Domain.Users;

namespace zbw.Auftragsverwaltung.BlazorApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthenticationClient _authenticationClient;
        private readonly NavigationManager _navigationManager;
        private readonly IStorageService _storageService;

        private const string UserKey = "user";

        public AuthService(IAuftragsverwaltungClient auftragsverwaltungClient, NavigationManager navigationManager, IStorageService storageService)
        {
            _authenticationClient = auftragsverwaltungClient.Authentication();
            _navigationManager = navigationManager;
            _storageService = storageService;
        }

        public AuthenticateResponse User { get; set; }
        public async Task Initialize()
        {
            User = await _storageService.GetItem<AuthenticateResponse>(UserKey);
        }

        public async Task Login(string username, string password)
        {
            User = await _authenticationClient.Login(new AuthenticateRequest()
                {Password = password, Username = username});

            await _storageService.SetItem(UserKey, User);
        }

        public async Task Logout()
        {
            var result = await _authenticationClient.Logout();

            User = null;
            await _storageService.RemoveItem(UserKey);
            _navigationManager.NavigateTo("account/login");
        }

        public async void Refresh()
        {
            if(User == null)
                return;
            try
            {
                User = await _authenticationClient.RefreshToken(new RefreshTokenRequest() { Token = User.RefreshToken });
                
            }
            catch (Exception e)
            {
                await Logout();
                return;
            }

            await _storageService.SetItem(UserKey, User).ConfigureAwait(false);
        }

        public bool Validate()
        {
            var handler = new JwtSecurityTokenHandler();
            
            if (User == null)
                return false;

            try
            {
                var token = handler.ReadJwtToken(User.AuthenticationToken.ToString());

                if (token.ValidFrom <= DateTime.UtcNow && token.ValidTo >= DateTime.UtcNow)
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }
    }
}
