using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using zbw.Auftragsverwaltung.Lib.HttpClient.Helper;

namespace zbw.Auftragsverwaltung.Client.Common.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpRequestMessage AddAuthenticationHeaders(this HttpRequestMessage request, IContextDataService context)
        {
            var token = context.GetAuthorizationHeader().Replace("Bearer ", "");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return request;
        }
    }
}
