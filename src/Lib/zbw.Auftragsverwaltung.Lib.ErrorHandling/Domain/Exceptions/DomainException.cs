using System;
using System.Collections.Generic;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Exceptions;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Extensions;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Helpers;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Enumerations;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Models;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Exceptions
{
    public class DomainException : ProblemDetailsException
    {
        public DomainException(
            DomainErrorTypeEnumeration type,
            string title,
            int? status,
            string detail = null,
            string instance = null,
            Exception ex = null,
            params KeyValuePair<string, object>[] extensions) 
            : this(new DomainProblemDetails(type, title, status, detail, instance, extensions), ex)
        {

        }

        public DomainException(DomainProblemDetails details) : this(details, null)
        {
        }

        public DomainException(DomainProblemDetails details, Exception innerEx) : base(details, innerEx)
        {
            if (details != null)
                details.AddExtension(ErrorHandlerDefaults.IsDomain, true, true);
        }
    }
}
