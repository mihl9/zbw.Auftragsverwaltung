using System;
using System.Linq;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Enumerations;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Models
{
    public class ExceptionProblemDetails : StatusCodeProblemDetails
    {
        public ExceptionProblemDetails()
            : this(null)
        {
        }

        public ExceptionProblemDetails(Exception error)
        : this(error, StatusCodes.Status500InternalServerError)
        {
            Type = DomainErrorTypeEnumeration.InternalServerError.ErrorType.Name;
            Title = "Internal server error.";

        }

        public ExceptionProblemDetails(Exception error, int statusCode, ProblemDetails problem = null)
            : base(statusCode)
        {
            Error = error;

            if (problem != null)
            {
                Detail = problem.Detail;
                Title = problem.Title;
                Instance = problem.Instance;
                Type = problem.Type;
                if (problem.Extensions != null && problem.Extensions.Any())
                {
                    problem.Extensions.Keys.ToList().ForEach(k =>
                    {
                        Extensions.Add(k, problem.Extensions[k]);
                    });
                }
            }
        }

        [JsonIgnore]
        public Exception Error { get; }

    }
}
