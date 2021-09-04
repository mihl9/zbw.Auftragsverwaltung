using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using zbw.Auftragsverwaltung.Client.Common.Configuration;
using zbw.Auftragsverwaltung.Domain.Users;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Helpers;
using zbw.Auftragsverwaltung.Lib.HttpClient.Extensions;
using zbw.Auftragsverwaltung.Lib.HttpClient.Helper;

namespace zbw.Auftragsverwaltung.Client.Authentication
{
    public class AuthenticationClient : IAuthenticationClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly IContextDataService _contextDataService;
        private readonly HttpExceptionMapper _exceptionMapper;

        public AuthenticationClient(HttpClient httpClient, string baseUrl, IContextDataService contextDataService, HttpExceptionMapper exceptionMapper)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;
            _contextDataService = contextDataService;
            _exceptionMapper = exceptionMapper;
        }

        public async Task<bool> Register(RegisterRequest request)
        {
            var builder = new UriBuilder(_baseUrl);
            builder.Path = "api/auth/register";

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, builder.ToString());
            httpRequest.Content = JsonContent.Create(request);

            var response = await _httpClient.SendAsync(httpRequest);

            await response.EnsureSuccess(_exceptionMapper);

            return true;
        }

        public async Task<AuthenticateResponse> Login(AuthenticateRequest request)
        {
            var builder = new UriBuilder(_baseUrl);
            builder.Path = "api/auth/login";

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, builder.ToString());
            httpRequest.Content = JsonContent.Create(request);

            var response = await _httpClient.SendAsync(httpRequest);

            await response.EnsureSuccess(_exceptionMapper);

            return await response.Content.ReadFromJsonAsync<AuthenticateResponse>();
        }

        public async Task<bool> Logout()
        {
            var builder = new UriBuilder(_baseUrl);
            builder.Path = "api/auth/logout";

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, builder.ToString());

            var response = await _httpClient.SendAsync(httpRequest);

            await response.EnsureSuccess(_exceptionMapper);

            return true;
        }

        public async Task<AuthenticateResponse> RefreshToken(RefreshTokenRequest request)
        {
            var builder = new UriBuilder(_baseUrl);
            builder.Path = "api/auth/refresh-token";

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, builder.ToString());
            httpRequest.Content = JsonContent.Create(request);

            var response = await _httpClient.SendAsync(httpRequest);

            await response.EnsureSuccess(_exceptionMapper);

            return await response.Content.ReadFromJsonAsync<AuthenticateResponse>();
        }

        public async Task<bool> RevokeToken(RevokeTokenRequest request)
        {
            var builder = new UriBuilder(_baseUrl);
            builder.Path = "api/auth/revoke-token";

            var httpRequest = new HttpRequestMessage(HttpMethod.Post, builder.ToString());
            httpRequest.Content = JsonContent.Create(request);

            var response = await _httpClient.SendAsync(httpRequest);

            await response.EnsureSuccess(_exceptionMapper);

            return true;
        }
    }
}
