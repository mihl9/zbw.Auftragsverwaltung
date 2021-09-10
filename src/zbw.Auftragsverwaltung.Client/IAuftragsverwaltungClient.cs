using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Client.ArticleGroup;
using zbw.Auftragsverwaltung.Client.Authentication;
using zbw.Auftragsverwaltung.Client.Customer;
using zbw.Auftragsverwaltung.Client.Invoices;
using zbw.Auftragsverwaltung.Client.Report;
using zbw.Auftragsverwaltung.Client.User;

namespace zbw.Auftragsverwaltung.Client
{
    public interface IAuftragsverwaltungClient
    {
        IArticleGroupClient ArticleGroup();

        IAuthenticationClient Authentication();

        ICustomerClient Customer();

        IUserClient User();

        IReportClient Report();

        IInvoiceClient Invoice();
    }
}
