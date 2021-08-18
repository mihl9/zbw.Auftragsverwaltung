using System.Collections.Generic;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Enumerations;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Exceptions
{
    public class HttpUnauthorizedException : HttpDomainException
    {
        public HttpUnauthorizedException(string title, string detail = null) 
            : base(DomainErrorTypeEnumeration.InternalServerError, title, 401, detail, null, null, new KeyValuePair<string, object>())
        {
        }
    }
}
