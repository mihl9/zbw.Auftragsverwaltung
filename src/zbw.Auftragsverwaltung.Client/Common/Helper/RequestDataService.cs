using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http;
using zbw.Auftragsverwaltung.Client.Common.Model;

namespace zbw.Auftragsverwaltung.Client.Common.Helper
{
    public class RequestDataService : IRequestDataService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestDataService(IHttpContextAccessor httpContextAccessor)
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
            throw new NotImplementedException();
        }

        public RequestContext GetRequestContext()
        {
            return new RequestContext
            {
                Authorization = GetAuthorizationHeader()
            };

        }

        public void AddToHttpRequest(HttpRequestMessage httpRequestMessage)
        {
            throw new NotImplementedException();
        }
    }
}
