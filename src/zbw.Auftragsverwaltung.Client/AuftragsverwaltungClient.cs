using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Options;
using zbw.Auftragsverwaltung.Client.Address;
using zbw.Auftragsverwaltung.Client.ArticleGroup;
using zbw.Auftragsverwaltung.Client.Authentication;
using zbw.Auftragsverwaltung.Client.Common.Configuration;
using zbw.Auftragsverwaltung.Client.Customer;
using zbw.Auftragsverwaltung.Client.Invoices;
using zbw.Auftragsverwaltung.Client.Report;
using zbw.Auftragsverwaltung.Client.User;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Contracts;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Helpers;
using zbw.Auftragsverwaltung.Lib.HttpClient.Helper;

namespace zbw.Auftragsverwaltung.Client
{
    public class AuftragsverwaltungClient : IAuftragsverwaltungClient
    {
        private readonly IAuthenticationClient _authenticationClient;
        private readonly ICustomerClient _customerClient;
        private readonly IReportClient _reportClient;
        private readonly IInvoiceClient _invoiceClient;
        private readonly IAddressClient _addressClient;

        public AuftragsverwaltungClient(IHttpClientFactory httpClientFactory,
            IOptions<AuftragsverwaltungClientConfiguration> configuration, IContextDataService contextDataService, IExceptionMapper<HttpResponseMessage> exceptionMapper)
        {
            var client = httpClientFactory.CreateClient(configuration.Value.ClientName);

            _authenticationClient = new AuthenticationClient(client, configuration.Value.BackendServiceEndpoint,
                contextDataService, exceptionMapper);

            _customerClient = new CustomerClient(client, configuration.Value.BackendServiceEndpoint, contextDataService,
                exceptionMapper);
            _reportClient = new ReportClient(client, configuration.Value.BackendServiceEndpoint, contextDataService,
                exceptionMapper);

            _invoiceClient = new InvoiceClient(client, configuration.Value.BackendServiceEndpoint, contextDataService,
                exceptionMapper);

            _addressClient = new AddressClient(client, configuration.Value.BackendServiceEndpoint, contextDataService,
                exceptionMapper);
        }

        public IArticleGroupClient ArticleGroup()
        {
            throw new NotImplementedException();
        }

        public IAuthenticationClient Authentication()
        {
            return _authenticationClient;
        }

        public ICustomerClient Customer()
        {
            return _customerClient;
        }

        public IUserClient User()
        {
            throw new NotImplementedException();
        }

        public IReportClient Report()
        {
            return _reportClient;
        }

        public IInvoiceClient Invoice()
        {
            return _invoiceClient;
        }

        public IAddressClient Address()
        {
            return _addressClient;
        }
    }
}
