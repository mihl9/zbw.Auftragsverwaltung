using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Domain.Users;

namespace zbw.Auftragsverwaltung.BlazorApp.Services
{
    public interface IAuthService
    {
        public AuthenticateResponse User { get; set; }

        Task Initialize();
        Task Login(string username, string password);
        Task Logout();

        void Refresh();

        bool Validate();
    }
}
