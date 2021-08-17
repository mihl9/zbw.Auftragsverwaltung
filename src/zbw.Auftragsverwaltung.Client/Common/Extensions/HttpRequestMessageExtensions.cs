using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace zbw.Auftragsverwaltung.Client.Common.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        public static HttpRequestMessage AddAuthenticationHeaders(this HttpRequestMessage request, string token)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return request;
        }
    }
}
