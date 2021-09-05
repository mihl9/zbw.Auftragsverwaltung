using System;
using System.Collections.Generic;
using System.Text;

namespace zbw.Auftragsverwaltung.Client.Common.Configuration
{
    public class AuftragsverwaltungClientConfiguration
    {
        public string BackendServiceEndpoint { get; set; }

        public int TimeoutInSeconds { get; set; } = 30;

        public string ClientName { get; set; } = "Default-Client";
    }
}
