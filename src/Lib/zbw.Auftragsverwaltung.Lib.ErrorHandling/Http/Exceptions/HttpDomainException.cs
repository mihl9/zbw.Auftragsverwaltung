using System;
using System.Collections.Generic;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Enumerations;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Exceptions;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Models;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Exceptions
{
    public class HttpDomainException : DomainException
    {
        public HttpDomainException(DomainErrorTypeEnumeration type, string title, int? status, string detail = null, string instance = null, Exception ex = null, params KeyValuePair<string, object>[] extensions) : base(type, title, status, detail, instance, ex, extensions)
        {
        }

        public HttpDomainException(DomainProblemDetails details) : base(details)
        {
        }

        public HttpDomainException(DomainProblemDetails details, Exception innerEx) : base(details, innerEx)
        {
        }
    }
}
