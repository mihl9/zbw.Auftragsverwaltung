using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Contracts;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Models;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Helpers;
using zbw.Auftragsverwaltung.Lib.HttpClient.Helper;

namespace zbw.Auftragsverwaltung.Lib.HttpClient.Extensions
{
    public static class HttpRequestMessageExtensions
    {
        public static async Task<HttpRequestMessage> AddAuthenticationHeaders(this HttpRequestMessage request, IContextDataService context)
        {
            var token = (await context.GetAuthorizationHeader()).Replace("Bearer ", "");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return request;
        }

        public static async Task<HttpResponseMessage> EnsureSuccess(this HttpResponseMessage response, IExceptionMapper<HttpResponseMessage> mapper)
        {
            await mapper.EnsureSuccess(new ResponseWrapper<HttpResponseMessage>(response));
            return response;
        }
    }
}
