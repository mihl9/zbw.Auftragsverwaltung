using System.Net.Http;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Lib.HttpClient.Model;

namespace zbw.Auftragsverwaltung.Lib.HttpClient.Helper
{
    public interface IContextDataService
    {
        Task<string> GetAuthorizationHeader();

        Task<RequestContext> GetRequestContext();

    }
}
