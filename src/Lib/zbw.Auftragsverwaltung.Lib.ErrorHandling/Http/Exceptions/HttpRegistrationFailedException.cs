using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Enumerations;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Exceptions
{
    public sealed class HttpRegistrationFailedException : HttpDomainException
    {
        public HttpRegistrationFailedException(string title, string detail = null, object states = null)
            : base(DomainErrorTypeEnumeration.CustomServerError, title, (int)HttpStatusCode.BadRequest, detail, null, null, new KeyValuePair<string, object>("states", states))
        {
        }
    }
}
