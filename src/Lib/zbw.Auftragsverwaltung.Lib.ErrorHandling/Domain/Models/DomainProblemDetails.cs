using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Models;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Enumerations;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Exceptions;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Models
{
    public class DomainProblemDetails : ProblemDetails
    {
        public DomainProblemDetails(
            DomainErrorTypeEnumeration errorType,
            string title,
            int? status = null,
            string detail = null,
            string instance = null,
            params KeyValuePair<string, object>[] extensions)
        {
            Type = errorType?.ErrorType.Name ?? throw new ArgumentNullException(nameof(errorType));
            ErrorDescriptor = errorType.ErrorType;
            Title = title;
            Status = status;
            Detail = detail;
            Instance = instance;

            if (extensions != null && extensions.Any())
            {
                extensions.ToList().ForEach(e =>
                {
                    if (e.Key == null)
                    {
                        return;
                    }

                    if (Extensions.ContainsKey(e.Key))
                    {
                        Extensions[e.Key] = e.Value;
                    }
                    else
                    {
                        Extensions.Add(e.Key, e.Value);
                    }
                });
            }
        }

        [JsonIgnore]
        public ErrorBaseType ErrorDescriptor { get; set; }

    }
}
