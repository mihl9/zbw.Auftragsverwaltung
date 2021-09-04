using System.Net.Http;
using System.Net.Http.Headers;
using zbw.Auftragsverwaltung.Lib.HttpClient.Helper;

namespace zbw.Auftragsverwaltung.Lib.HttpClient.Extensions
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
