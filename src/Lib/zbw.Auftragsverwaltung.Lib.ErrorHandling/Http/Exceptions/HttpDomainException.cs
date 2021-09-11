using System;
using System.Collections.Generic;
using System.Linq;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Exceptions;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Extensions;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Helpers;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Models;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Enumerations;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Exceptions;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Models;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Exceptions
{
    public class HttpDomainException : DomainException
    {
        public HttpDomainException(DomainErrorTypeEnumeration type, string title, int? status, string detail = null, string instance = null, Exception ex = null, params KeyValuePair<string, object>[] extensions) : base(type, title, status, detail, instance, ex, extensions)
        {
            if (extensions != null && extensions.Any())
            {
                extensions.ToList().ForEach(e =>
                {
                    if (e.Key == null)
                    {
                        return;
                    }

                    if (Details.Extensions.ContainsKey(e.Key))
                    {
                        Details.Extensions[e.Key] = e.Value;
                    }
                    else
                    {
                        Details.Extensions.Add(e.Key, e.Value);
                    }
                });
            }
        }

        public HttpDomainException(DomainProblemDetails details) : base(details)
        {
        }

        public HttpDomainException(DomainProblemDetails details, Exception innerEx) : base(details, innerEx)
        {
            
        }
    }
}
