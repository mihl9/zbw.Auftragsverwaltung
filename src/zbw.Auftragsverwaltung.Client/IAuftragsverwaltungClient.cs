using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Client.Address;
using zbw.Auftragsverwaltung.Client.Article;
using zbw.Auftragsverwaltung.Client.ArticleGroup;
using zbw.Auftragsverwaltung.Client.Authentication;
using zbw.Auftragsverwaltung.Client.Customer;
using zbw.Auftragsverwaltung.Client.Invoices;
using zbw.Auftragsverwaltung.Client.Order;
using zbw.Auftragsverwaltung.Client.Positions;
using zbw.Auftragsverwaltung.Client.Report;
using zbw.Auftragsverwaltung.Client.User;

namespace zbw.Auftragsverwaltung.Client
{
    public interface IAuftragsverwaltungClient
    {
        IAddressClient Address();

        IArticleClient Article();

        IArticleGroupClient ArticleGroup();

        IAuthenticationClient Authentication();

        ICustomerClient Customer();

        IInvoiceClient Invoice();

        IOrderClient Order();

        IPositionClient Position();

        IReportClient Report();

        IUserClient User();

        


        
    }
}
