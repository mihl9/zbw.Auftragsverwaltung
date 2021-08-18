using System.Net.Http;
using zbw.Auftragsverwaltung.Lib.HttpClient.Model;

namespace zbw.Auftragsverwaltung.Lib.HttpClient.Helper
{
    public interface IContextDataService
    {
        string GetAuthorizationHeader();

        RequestContext GetRequestContext();

    }
}
