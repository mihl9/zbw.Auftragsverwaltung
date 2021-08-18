using System;
using System.Collections.Generic;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Enumerations;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Models;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Exceptions
{
    public class InternalDomainException : DomainException
    {
        public InternalDomainException(string title, string detail = null, string instance = null, Exception ex = null)
            : base(DomainErrorTypeEnumeration.InternalServerError, title, 401, detail, instance, ex, new KeyValuePair<string, object>())
        {
        }
    }
}
