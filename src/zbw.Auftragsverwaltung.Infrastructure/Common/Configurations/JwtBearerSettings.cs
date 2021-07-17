using System;
using System.Collections.Generic;
using System.Text;

namespace zbw.Auftragsverwaltung.Infrastructure.Common.Configurations
{
    public class JwtBearerSettings
    {
        public string Secret { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpiryTimeInSeconds { get; set; }
    }
}
