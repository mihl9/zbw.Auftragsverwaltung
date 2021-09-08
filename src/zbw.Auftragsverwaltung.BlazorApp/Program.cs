using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MudBlazor;
using MudBlazor.Services;
using Tewr.Blazor.FileReader;
using zbw.Auftragsverwaltung.BlazorApp.Helpers;
using zbw.Auftragsverwaltung.BlazorApp.Services;
using zbw.Auftragsverwaltung.Client;
using zbw.Auftragsverwaltung.Client.Common.Configuration;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Contracts;
using zbw.Auftragsverwaltung.Lib.HttpClient.Helper;

namespace zbw.Auftragsverwaltung.BlazorApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var services = builder.Services;

            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            services.AddHttpClient();

            services.AddAuftragsverwaltungClientService<BlazorContextDataService>();
            services.AddOptions();

            services.Configure<AuftragsverwaltungClientConfiguration>(x =>
            {
                builder.Configuration.GetSection("AuftragsverwaltungClient").Bind(x);
            });

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IStorageService, LocalStorageService>();
            services.AddScoped<INavMenuStateService, NavMenuStateService>();

            services.AddMudServices(o =>
            {
                o.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
            });

            services.AddFileReaderService(options => options.UseWasmSharedBuffer = true);

            var host = builder.Build();

            var authenticationService = host.Services.GetRequiredService<IAuthService>();
            await authenticationService.Initialize();

            var httpExceptionMapper = host.Services.GetRequiredService<IExceptionMapper<HttpResponseMessage>>();
            
            await host.RunAsync();
        }
    }
}
