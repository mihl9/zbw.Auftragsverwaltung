using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Contracts;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Helpers;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Models;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Enumerations;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Exceptions;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Helpers
{
    public class HttpExceptionMapper : IExceptionMapper<HttpResponseMessage>
    {
        private static readonly IDictionary<string, Func<ProblemDetails, Exception>> Types =
            new Dictionary<string, Func<ProblemDetails, Exception>>();

        public void AddMappingIfNotExists(string exceptionName, Func<ProblemDetails, Exception> exceptionMapping)
        {
            if(Types.ContainsKey(exceptionName))
                return;
            Types.Add(exceptionName, exceptionMapping);
        }

        public async Task EnsureSuccess(ResponseWrapper<HttpResponseMessage> wrapper)
        {
            var response = wrapper.Response;
            if (response.IsSuccessStatusCode)
                return;

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new HttpUnauthorizedException(response.ReasonPhrase);

            var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            if (problemDetails == null)
                throw new HttpDomainException(DomainErrorTypeEnumeration.InternalServerError, "Unknown Error", 500);

            if (!problemDetails.Extensions.TryGetValue(ErrorHandlerDefaults.ExceptionType, out var exceptionType) ||
                !Types.ContainsKey(exceptionType.ToString()))
            {
                throw new HttpDomainException(DomainErrorTypeEnumeration.InternalServerError, problemDetails.Title,
                    problemDetails.Status, problemDetails.Detail, problemDetails.Instance,
                    extensions: problemDetails.Extensions.ToArray());
            }

            var func = Types[exceptionType.ToString()];
            var exception = func(problemDetails);
            throw exception;
        }
    }
}
