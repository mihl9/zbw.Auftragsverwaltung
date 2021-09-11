using System.Collections.Generic;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Enumerations;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Models;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Exceptions
{
    public sealed class HttpUnauthorizedException : HttpDomainException
    {
        public HttpUnauthorizedException(string title, string detail = null) 
            : base(DomainErrorTypeEnumeration.CustomServerError, title, 401, detail, null, null, new KeyValuePair<string, object>())
        {
        }

        public HttpUnauthorizedException(DomainProblemDetails details) : base(details)
        {
        }
    }
}
