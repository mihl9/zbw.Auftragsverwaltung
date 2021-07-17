using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Api.Common.Models;

namespace zbw.Auftragsverwaltung.Api.Authentication.Models
{
    public class AuthenticatedMessage : BaseMessage
    {
        public object Token { get; set; }
    }
}
