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

namespace zbw.Auftragsverwaltung.Client
{
    public class AuftragsverwaltungClient : IAuftragsverwaltungClient
    {
        public AuftragsverwaltungClient(IHttpClientFactory httpClientFactory,
            IOptions<AuftragsverwaltungClientConfiguration> configuration)
        {
            var client = httpClientFactory.CreateClient(configuration.Value.ClientName);

        }

        public IArticleGroupClient ArticleGroup()
        {
            throw new NotImplementedException();
        }

        public IAuthenticationClient Authentication()
        {
            throw new NotImplementedException();
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
