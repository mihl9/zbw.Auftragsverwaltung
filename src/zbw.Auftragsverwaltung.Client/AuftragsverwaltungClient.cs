using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Options;
using zbw.Auftragsverwaltung.Client.ArticleGroup;
using zbw.Auftragsverwaltung.Client.Authentication;
using zbw.Auftragsverwaltung.Client.Common.Configuration;
using zbw.Auftragsverwaltung.Client.Customer;
using zbw.Auftragsverwaltung.Client.User;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Helpers;
using zbw.Auftragsverwaltung.Lib.HttpClient.Helper;

namespace zbw.Auftragsverwaltung.Client
{
    public class AuftragsverwaltungClient : IAuftragsverwaltungClient
    {
        private readonly IAuthenticationClient _authenticationClient;

        public AuftragsverwaltungClient(IHttpClientFactory httpClientFactory,
            IOptions<AuftragsverwaltungClientConfiguration> configuration, IContextDataService contextDataService, HttpExceptionMapper exceptionMapper)
        {
            var client = httpClientFactory.CreateClient(configuration.Value.ClientName);

            _authenticationClient = new AuthenticationClient(client, configuration.Value.BackendServiceEndpoint,
                contextDataService, exceptionMapper);
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
            throw new NotImplementedException();
        }

        public IUserClient User()
        {
            throw new NotImplementedException();
        }
    }
}
