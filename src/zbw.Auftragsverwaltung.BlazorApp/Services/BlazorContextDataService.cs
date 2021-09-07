using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Domain.Users;
using zbw.Auftragsverwaltung.Lib.HttpClient.Helper;
using zbw.Auftragsverwaltung.Lib.HttpClient.Model;

namespace zbw.Auftragsverwaltung.BlazorApp.Services
{
    public class BlazorContextDataService : IContextDataService
    {
        private readonly IStorageService _storageService;

        public BlazorContextDataService(IStorageService storageService)
        {
            _storageService = storageService;
        }

        public async Task<string> GetAuthorizationHeader()
        {
            return (await _storageService.GetItem<AuthenticateResponse>("user")).AuthenticationToken.ToString();
        }

        public async Task<RequestContext> GetRequestContext()
        {
            return new RequestContext
            {
                Authorization = await GetAuthorizationHeader()
            };
        }
    }
}
