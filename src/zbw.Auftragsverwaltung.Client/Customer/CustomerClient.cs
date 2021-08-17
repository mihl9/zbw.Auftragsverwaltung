using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using zbw.Auftragsverwaltung.Client.Common.Configuration;
using zbw.Auftragsverwaltung.Domain.Customers;

namespace zbw.Auftragsverwaltung.Client.Customer
{
    public class CustomerClient : ICustomerClient
    {

        private readonly HttpClient _httpClient;
        private readonly IOptions<AuftragsverwaltungClientConfiguration> _configuration;
        private readonly string _baseUrl;
        public CustomerClient(IOptions<AuftragsverwaltungClientConfiguration> configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
            _baseUrl = _configuration.Value.BackendServiceEndpoint;
        }

        public async Task<CustomerDto> Get(Guid id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/api/customer?id={id}");
            //TODO: Implement Authentication
            var response = await _httpClient.SendAsync(request);
            
            return await response.Content.ReadFromJsonAsync<CustomerDto>();

        }
    }
}
