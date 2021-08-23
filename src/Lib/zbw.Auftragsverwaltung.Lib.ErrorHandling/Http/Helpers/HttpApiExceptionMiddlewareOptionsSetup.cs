using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Configuration;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Helpers
{
    public class HttpApiExceptionMiddlewareOptionsSetup : IConfigureOptions<ApiExceptionMiddlewareOptions<HttpContext>>
    {
        public void Configure(ApiExceptionMiddlewareOptions<HttpContext> options)
        {
            if (options.RequestPathFilter == null)
            {
                options.RequestPathFilter = app => "/api";
            }

            if (options.IncludeExceptionDetails == null)
            {
                options.IncludeExceptionDetails = ctx => ctx.RequestServices.GetRequiredService<IHostingEnvironment>().IsDevelopment();
            }

            if (options.IncludeExceptionCode == null)
            {
                options.IncludeExceptionCode = ctx => true;
            }

            if (options.IncludeTraceIdentifier == null)
            {
                options.IncludeTraceIdentifier = ctx => true;
            }

            if (options.IncludeExceptionName == null)
            {
                options.IncludeExceptionName = ctx => true;
            }

            if (options.OverwriteExistingExtensions == null)
            {
                options.OverwriteExistingExtensions = ctx => true;
            }
        }

    }
}
