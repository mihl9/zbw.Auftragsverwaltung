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
using zbw.Auftragsverwaltung.Lib.HttpClient.Extensions;
using zbw.Auftragsverwaltung.Lib.HttpClient.Helper;

namespace zbw.Auftragsverwaltung.Client.Customer
{
    public class CustomerClient : ICustomerClient
    {

        private readonly HttpClient _httpClient;
        private readonly IOptions<AuftragsverwaltungClientConfiguration> _configuration;
        private readonly string _baseUrl;
        private readonly IContextDataService _contextDataService;
        public CustomerClient(IOptions<AuftragsverwaltungClientConfiguration> configuration, HttpClient httpClient, IContextDataService contextDataService)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _contextDataService = contextDataService;
            _baseUrl = _configuration.Value.BackendServiceEndpoint;
        }

        public async Task<CustomerDto> Get(Guid id)
        {
            var builder = new UriBuilder(_baseUrl);
            builder.Path = "api/customer";

            var query = HttpUtility.ParseQueryString(builder.Query);
            query.Add("id", id.ToString());
            var request = new HttpRequestMessage(HttpMethod.Get, builder.Uri);
            request.AddAuthenticationHeaders(_contextDataService);
            var response = await _httpClient.SendAsync(request);
            
            return await response.Content.ReadFromJsonAsync<CustomerDto>();

        }
    }
}
