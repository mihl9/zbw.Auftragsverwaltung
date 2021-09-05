using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Models;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Exceptions
{
    public class ProblemDetailsException : Exception
    {
        public ProblemDetailsException(ProblemDetails details, Exception innerEx) : base($"[{details?.Type}] {details?.Title} {details?.Detail}", innerEx)
        {
            Details = details;
        }

        public  ProblemDetails Details { get; protected set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Type     : {Details?.Type}");
            stringBuilder.AppendLine($"Title    : {Details?.Title}");
            stringBuilder.AppendLine($"Status   : {Details?.Status}");
            stringBuilder.AppendLine($"Detail   : {Details?.Detail}");
            stringBuilder.AppendLine($"Instance : {Details?.Instance}");

            if (Details?.Extensions != null && Details.Extensions.Keys.Any())
            {
                stringBuilder.AppendLine($"Extensions: {JsonSerializer.Serialize(Details.Extensions)}");
            }

            return stringBuilder.ToString();
        }
    }
}
