using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Client;

namespace zbw.Auftragsverwaltung.WebApp.Services
{
    public class AuthenticationService
    {
        private readonly IAuftragsverwaltungClient _auftragsverwaltungClient;

        public AuthenticationService(IAuftragsverwaltungClient auftragsverwaltungClient)
        {
            _auftragsverwaltungClient = auftragsverwaltungClient;
        }
    }
}
