using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Configuration;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Exceptions;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Extensions;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Helpers;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Models;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Exceptions;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Domain.Models;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Helpers;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Middleware
{
    public class HttpApiExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly ApiExceptionMiddlewareOptions<HttpContext> _options;

        public HttpApiExceptionMiddleware(
            RequestDelegate next,
            ILogger<HttpApiExceptionMiddleware> logger,
            IOptions<ApiExceptionMiddlewareOptions<HttpContext>> options)
        {
            _logger = logger;
            _next = next;
            _options = options?.Value ?? new ApiExceptionMiddlewareOptions<HttpContext>();
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleProblemDetail(httpContext, exception: ex);
            }
        }

        private Task HandleProblemDetail(HttpContext httpContext, Exception exception)
        {
            var exceptionCode = Guid.NewGuid().ToString();
            _logger.LogException(LogLevel.Error, "An exception occured", exceptionCode, httpContext, exception);

            HttpResponseExceptionHelper.RegisterExceptionCodeHeader(httpContext, exceptionCode);

            var ctxHttpStatus = httpContext.Response.StatusCode;
            var problemDetail = GetProblemDetail(exception);

            if (problemDetail is ExceptionProblemDetails exceptionProblemDetails)
            {
                // Handle domain and non domain exceptions
                if (exceptionProblemDetails.Error is DomainException domainException)
                {
                    problemDetail = new DomainExceptionDetails(domainException);
                }
                else
                {
                    // Use default status code details for all non-domain exceptions (hide sensitive server side data)
                    problemDetail = new StatusCodeProblemDetails(exceptionProblemDetails.Status ?? ctxHttpStatus);
                }
            }

            if (problemDetail.Extensions.ContainsKey(ErrorHandlerDefaults.ExceptionCode))
            {
                HttpResponseExceptionHelper.RegisterExceptionCodeHeader(httpContext, problemDetail.Extensions[ErrorHandlerDefaults.ExceptionCode].ToString());
            }

            var overwriteExtensions = _options.OverwriteExistingExtensions(httpContext);

            if (_options.IncludeExceptionDetails(httpContext))
            {
                ApplyExceptionDetails(problemDetail, exception, overwriteExtensions);
            }

            if (_options.IncludeExceptionCode(httpContext))
            {
                problemDetail.AddExtension(ErrorHandlerDefaults.ExceptionCode, exceptionCode, overwriteExtensions);
            }

            if (_options.IncludeTraceIdentifier(httpContext))
            {
                problemDetail.AddExtension(ErrorHandlerDefaults.TraceIdentifier, httpContext.TraceIdentifier, overwriteExtensions);
            }

            if (_options.IncludeExceptionName(httpContext))
            {
                problemDetail.AddExtension(ErrorHandlerDefaults.ExceptionType, exception.GetType().Name, overwriteExtensions);
            }

            httpContext.Response.ContentType = "application/problem+json";
            httpContext.Response.StatusCode = problemDetail.Status.Value;
            var result = JsonSerializer.Serialize(problemDetail);

            return httpContext.Response.WriteAsync(result);
        }

        private ProblemDetails GetProblemDetail(Exception exception)
        {
            // the exception already provides problem detail information which is taken over as is.
            if (exception is ProblemDetailsException problem)
            {
                return new ExceptionProblemDetails(exception, problem.Details.Status ?? StatusCodes.Status400BadRequest, problem.Details);
            }

            // a generic exception is mapped based on the custom middleware's options
            if (_options.TryMapProblemDetails(exception, out var result))
            {
                return result;
            }

            // fallback problems are often caused through non business logic related server side errors and
            // are reported with a 500 internal server error.
            return new ExceptionProblemDetails(exception);
        }

        private void ApplyExceptionDetails(ProblemDetails problemDetail, Exception ex, bool overwrite)
        {
            if (ex == null || problemDetail == null) return;

            if (!string.IsNullOrWhiteSpace(ex.Message)) problemDetail.AddExtension(ErrorHandlerDefaults.Message, ex.Message, overwrite);
            if (!string.IsNullOrWhiteSpace(ex.StackTrace)) problemDetail.AddExtension(ErrorHandlerDefaults.StackTrace, ex.StackTrace, overwrite);
            if (!string.IsNullOrWhiteSpace(ex.Source)) problemDetail.AddExtension(ErrorHandlerDefaults.Source, ex.Source, overwrite);
            if (!string.IsNullOrWhiteSpace(ex.TargetSite?.Name)) problemDetail.AddExtension(ErrorHandlerDefaults.Method, ex.TargetSite?.Name, overwrite);
            if (!string.IsNullOrWhiteSpace(ex.InnerException?.Message)) problemDetail.AddExtension(ErrorHandlerDefaults.InnerException, ex.InnerException?.Message, overwrite);
        }


        
    }
}
