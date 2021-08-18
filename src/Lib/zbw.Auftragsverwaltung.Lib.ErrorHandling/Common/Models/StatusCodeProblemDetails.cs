using Microsoft.AspNetCore.WebUtilities;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Helpers;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Models
{
    public class StatusCodeProblemDetails : ProblemDetails
    {
        public StatusCodeProblemDetails(int statusCode)
        {
            Status = statusCode;
            Type = $"https://httpstatuses.com/{statusCode}";
            Title = ReasonPhrases.GetReasonPhrase(statusCode);
        }

        public StatusCodeProblemDetails(int statusCode, string message, string exceptionCode, string traceIdentifier)
        {
            Status = statusCode;
            Type = $"https://httpstatuses.com/{statusCode}";
            Title = ReasonPhrases.GetReasonPhrase(statusCode);
            Detail = message;

            Extensions.Add(ErrorHandlerDefaults.ExceptionCode, exceptionCode);
            Extensions.Add(ErrorHandlerDefaults.TraceIdentifier, traceIdentifier);
        }

    }
}
