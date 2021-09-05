using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Configuration;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Helpers;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Middleware;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Extensions
{
    public static class HttpApiExceptionMiddlewareExtension
    {
        public static IServiceCollection AddHttpApiExceptionMiddleware(this IServiceCollection services, Action<ApiExceptionMiddlewareOptions<HttpContext>> configure)
        {
            if (configure != null)
            {
                services.Configure(configure);
            }

            services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<ApiExceptionMiddlewareOptions<HttpContext>>, HttpApiExceptionMiddlewareOptionsSetup>());

            return services;
        }

        /// <summary>
        /// This extension method registers a conditional middelware for api calls and handles
        /// all api specific exceptions starting with route '/api'.
        /// </summary>
        public static void UseHttpApiExceptionMiddleware(this IApplicationBuilder app)
        {
            if (app is null) throw new ArgumentNullException(nameof(app));

            var options = app.ApplicationServices.GetRequiredService<IOptions<ApiExceptionMiddlewareOptions<HttpContext>>>();

            app.UseWhen(context => context.Request.Path.StartsWithSegments(options.Value.RequestPathFilter(app), StringComparison.OrdinalIgnoreCase), appBuilder =>
            {
                appBuilder.UseMiddleware<HttpApiExceptionMiddleware>();
            });
        }

    }
}
