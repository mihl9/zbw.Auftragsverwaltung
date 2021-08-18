using System;
using System.Collections.Generic;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Enumerations;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Models;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Exceptions
{
    public class InternalDomainException : DomainException
    {
        public InternalDomainException(DomainErrorTypeEnumeration type, string title, int? status, string detail = null, string instance = null, Exception ex = null, params KeyValuePair<string, object>[] extensions) : base(type, title, status, detail, instance, ex, extensions)
        {
        }

        public InternalDomainException(DomainProblemDetails details) : base(details)
        {
        }

        public InternalDomainException(DomainProblemDetails details, Exception innerEx) : base(details, innerEx)
        {
        }
    }
}
