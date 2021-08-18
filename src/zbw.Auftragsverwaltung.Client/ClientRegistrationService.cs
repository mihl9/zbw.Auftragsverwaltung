using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Contracts;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Helpers;
using zbw.Auftragsverwaltung.Lib.HttpClient.Helper;

namespace zbw.Auftragsverwaltung.Client
{
    public static class ClientRegistrationService
    {
        public static IServiceCollection AddClientService<TContext>(this IServiceCollection services) where TContext : class, IContextDataService
        {
            services.AddScoped<IContextDataService, TContext>();
            services.AddScoped<IAuftragsverwaltungClient, AuftragsverwaltungClient>();
            
            if(!services.Contains(new ServiceDescriptor(typeof(IExceptionMapper<HttpResponseMessage>), typeof(HttpExceptionMapper), ServiceLifetime.Singleton)))
            {
                services.AddSingleton<IExceptionMapper<HttpResponseMessage>, HttpExceptionMapper>();
            }
            
            return services;
        }
    }
}
