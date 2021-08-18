using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Enumerations;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Exceptions
{
    public class NotFoundByIdDomainException : DomainException
    {
        public NotFoundByIdDomainException(string title, string detail = null, string instance = null, Exception ex = null)
            : base(DomainErrorTypeEnumeration.EntityNotFoundById, title, 404, detail, instance, ex, new KeyValuePair<string, object>())
        {
        }
    }
}
