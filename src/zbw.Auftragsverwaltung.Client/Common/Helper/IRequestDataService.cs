using System.Net.Http;
using zbw.Auftragsverwaltung.Client.Common.Model;

namespace zbw.Auftragsverwaltung.Client.Common.Helper
{
    public interface IRequestDataService
    {
        string GetAuthorizationHeader();

        RequestContext GetRequestContext();

        void AddToHttpRequest(HttpRequestMessage httpRequestMessage);

    }
}
