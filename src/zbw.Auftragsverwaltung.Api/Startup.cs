using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using zbw.Auftragsverwaltung.Core;
using zbw.Auftragsverwaltung.Core.Common.Exceptions;
using zbw.Auftragsverwaltung.Infrastructure;
using zbw.Auftragsverwaltung.Infrastructure.Migrators;
using zbw.Auftragsverwaltung.Infrastructure.Users.Services;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Extensions;

namespace zbw.Auftragsverwaltung.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o =>
            {
                //really unsafe
                o.AddDefaultPolicy(p =>
                {
                    p.AllowAnyOrigin();
                    p.AllowAnyHeader();
                    p.AllowAnyMethod();
                });
            });

            services.AddOptions();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddHttpApiExceptionMiddleware(c =>
            {
                c.RequestPathFilter = (ctx) => "";
                c.IncludeTraceIdentifier = (ctx) => false;
                c.OverwriteExistingExtensions = ctx => false;
                c.IncludeExceptionName = ctx => true;
                c.Map<ArgumentException>(StatusCodes.Status400BadRequest);
                c.Map<JsonException>(StatusCodes.Status400BadRequest);
                c.Map<InvalidRightsException>(StatusCodes.Status403Forbidden);
                c.Map<UserNotFoundException>(StatusCodes.Status401Unauthorized);
                c.Map<NotFoundByIdException>(StatusCodes.Status404NotFound);
            });

            services.AddInfrastructurServices(Configuration);
            
            services.AddCoreServices();
            services.AddAuthenticationService<DefaultTokenService>(Configuration);
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider services)
        {
            app.UseHttpApiExceptionMiddleware();
            app.MigrateOrderDatabase();
            app.MigrateUserIdentityDatabase();
            //app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Order Management API V1");
                c.RoutePrefix = "api/Documentation";
            });
            
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors();
            app.UseDefaultRoles(services);

            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                
                IdentityModelEventSource.ShowPII = true;
            }

           

            //always generate test users
            app.UseDevUser(services);
            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
