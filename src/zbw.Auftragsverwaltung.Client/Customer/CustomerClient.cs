using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Options;
using zbw.Auftragsverwaltung.Client.Common.Configuration;
using zbw.Auftragsverwaltung.Domain.Customers;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Helpers;
using zbw.Auftragsverwaltung.Lib.HttpClient.Extensions;
using zbw.Auftragsverwaltung.Lib.HttpClient.Helper;

namespace zbw.Auftragsverwaltung.Client.Customer
{
    public class CustomerClient : ICustomerClient
    {

        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly IContextDataService _contextDataService;
        private readonly HttpExceptionMapper _exceptionMapper;
        public CustomerClient(HttpClient httpClient, string baseUrl, IContextDataService contextDataService, HttpExceptionMapper exceptionMapper)
        {
            _httpClient = httpClient;
            _contextDataService = contextDataService;
            _exceptionMapper = exceptionMapper;
            _baseUrl = baseUrl;
        }

        public async Task<CustomerDto> Get(Guid id)
        {
            var builder = new UriBuilder(_baseUrl)
            {
                Path = "api/customer"
            };

            var query = HttpUtility.ParseQueryString(builder.Query);
            query.Add("id", id.ToString());
            var request = new HttpRequestMessage(HttpMethod.Get, builder.Uri);
            request.AddAuthenticationHeaders(_contextDataService);
            var response = await _httpClient.SendAsync(request);
            
            await response.EnsureSuccess(_exceptionMapper);
            
            return await response.Content.ReadFromJsonAsync<CustomerDto>();

        }
    }
}
