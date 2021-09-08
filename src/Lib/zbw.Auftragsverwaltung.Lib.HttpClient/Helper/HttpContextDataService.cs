using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using zbw.Auftragsverwaltung.Lib.HttpClient.Model;

namespace zbw.Auftragsverwaltung.Lib.HttpClient.Helper
{
    public class HttpContextDataService : IContextDataService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextDataService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private async Task<string> GetHeaderValue(string headerName)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            return await Task.FromResult(request.Headers[headerName].ToString());
        }

        public async Task<string> GetAuthorizationHeader()
        {
            return await GetHeaderValue("Authorization");
        }

        public async Task<RequestContext> GetRequestContext()
        {
            return new RequestContext
            {
                Authorization = await GetAuthorizationHeader()
            };

        }

    }
}
