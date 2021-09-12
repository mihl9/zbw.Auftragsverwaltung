using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Options;
using zbw.Auftragsverwaltung.Client.Address;
using zbw.Auftragsverwaltung.Client.Article;
using zbw.Auftragsverwaltung.Client.ArticleGroup;
using zbw.Auftragsverwaltung.Client.Authentication;
using zbw.Auftragsverwaltung.Client.Common.Configuration;
using zbw.Auftragsverwaltung.Client.Customer;
using zbw.Auftragsverwaltung.Client.Invoices;
using zbw.Auftragsverwaltung.Client.Order;
using zbw.Auftragsverwaltung.Client.Position;
using zbw.Auftragsverwaltung.Client.Report;
using zbw.Auftragsverwaltung.Client.User;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Contracts;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Helpers;
using zbw.Auftragsverwaltung.Lib.HttpClient.Helper;

namespace zbw.Auftragsverwaltung.Client
{
    public class AuftragsverwaltungClient : IAuftragsverwaltungClient
    {
        private readonly IAddressClient _addressClient;
        private readonly IArticleClient _articleClient;
        private readonly IArticleGroupClient _articleGroupClient;
        private readonly IAuthenticationClient _authenticationClient;
        private readonly ICustomerClient _customerClient;
        private readonly IInvoiceClient _invoiceClient;
        private readonly IOrderClient _orderClient;
        private readonly IPositionClient _positionClient;
        private readonly IReportClient _reportClient;
        private readonly IUserClient _userClient;
        
        

        public AuftragsverwaltungClient(IHttpClientFactory httpClientFactory,
            IOptions<AuftragsverwaltungClientConfiguration> configuration, IContextDataService contextDataService, IExceptionMapper<HttpResponseMessage> exceptionMapper)
        {
            var client = httpClientFactory.CreateClient(configuration.Value.ClientName);

            _addressClient = new AddressClient(client, configuration.Value.BackendServiceEndpoint, contextDataService,
                exceptionMapper);

            _articleClient = new ArticleClient(client, configuration.Value.BackendServiceEndpoint, contextDataService,
                exceptionMapper);

            _articleGroupClient = new ArticleGroupClient(client, configuration.Value.BackendServiceEndpoint, contextDataService,
                exceptionMapper);

            _authenticationClient = new AuthenticationClient(client, configuration.Value.BackendServiceEndpoint,
                contextDataService, exceptionMapper);

            _customerClient = new CustomerClient(client, configuration.Value.BackendServiceEndpoint, contextDataService,
                exceptionMapper);

            _invoiceClient = new InvoiceClient(client, configuration.Value.BackendServiceEndpoint, contextDataService,
                exceptionMapper);

            _orderClient = new OrderClient(client, configuration.Value.BackendServiceEndpoint, contextDataService,
                exceptionMapper);

            _positionClient = new PositionClient(client, configuration.Value.BackendServiceEndpoint, contextDataService,
                exceptionMapper);

            _reportClient = new ReportClient(client, configuration.Value.BackendServiceEndpoint, contextDataService,
                exceptionMapper);

            _userClient = new UserClient(client, configuration.Value.BackendServiceEndpoint, contextDataService,
                exceptionMapper);
        }


        public IAddressClient Address()
        {
            return _addressClient;
        }

        public IArticleClient Article()
        {
            return _articleClient;
        }

        public IArticleGroupClient ArticleGroup()
        {
            return _articleGroupClient;
        }

        public IAuthenticationClient Authentication()
        {
            return _authenticationClient;
        }

        public ICustomerClient Customer()
        {
            return _customerClient;
        }

        public IInvoiceClient Invoice()
        {
            return _invoiceClient;
        }

        public IOrderClient Order()
        {
            return _orderClient;
        }

        public IPositionClient Position()
        {
            return _positionClient;
        }

        public IReportClient Report()
        {
            return _reportClient;
        }

        public IUserClient User()
        {
            return _userClient;
        }
    }
}
