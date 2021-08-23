using System;
using System.Net.Http;
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

        private string GetHeaderValue(string headerName)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            return request.Headers[headerName].ToString();
        }

        public string GetAuthorizationHeader()
        {
            return GetHeaderValue("Authorization");
        }

        public RequestContext GetRequestContext()
        {
            return new RequestContext
            {
                Authorization = GetAuthorizationHeader()
            };

        }

    }
}
