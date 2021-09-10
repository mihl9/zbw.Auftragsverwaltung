using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using zbw.Auftragsverwaltung.Core.Addresses.BLL;
using zbw.Auftragsverwaltung.Core.Addresses.Interfaces;
using zbw.Auftragsverwaltung.Core.Common.Configurations;
using zbw.Auftragsverwaltung.Core.ArticleGroups.BLL;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Interfaces;
using zbw.Auftragsverwaltung.Core.Orders.BLL;
using zbw.Auftragsverwaltung.Core.Orders.Interfaces;
using zbw.Auftragsverwaltung.Core.Positions.BLL;
using zbw.Auftragsverwaltung.Core.Positions.Interfaces;
using zbw.Auftragsverwaltung.Core.Articles.BLL;
using zbw.Auftragsverwaltung.Core.Articles.Interfaces;
using zbw.Auftragsverwaltung.Core.Customers.BLL;
using zbw.Auftragsverwaltung.Core.Customers.Interfaces;
using zbw.Auftragsverwaltung.Core.Invoices.BLL;
using zbw.Auftragsverwaltung.Core.Invoices.Interfaces;
using zbw.Auftragsverwaltung.Core.Users.Bll;
using zbw.Auftragsverwaltung.Core.Users.Contracts;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Core.Users.Interfaces;
using zbw.Auftragsverwaltung.Core.Reports.Interfaces;
using zbw.Auftragsverwaltung.Core.Reports.BLL;

namespace zbw.Auftragsverwaltung.Core
{
    public static class CoreRegistrationService
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<ICustomerBll, CustomerBll>();
            services.AddScoped<IAddressBll, AddressBll>();
            services.AddScoped<IUserBll, UserBll>();
            services.AddScoped<IOrderBll, OrderBll>();
            services.AddScoped<IPositionBll, PositionBll>();
            services.AddScoped<IArticleGroupBll, ArticleGroupBll>();
            services.AddScoped<IArticleBll, ArticleBll>();
            services.AddScoped<IInvoiceBll, InvoiceBll>();

            services.AddScoped<IReportBll, ReportBll>();

            return services;
        }

        public static IServiceCollection AddAuthenticationService<TTokenService>(this IServiceCollection services,
            IConfiguration configuration) where TTokenService : class, ITokenService
        {

            services.AddScoped<ITokenService, TTokenService>();

            var jwtSection = configuration.GetSection("JwtBearerSettings");

            services.Configure<JwtBearerSettings>(jwtSection);

            var settings = jwtSection.Get<JwtBearerSettings>();
            var key = Encoding.ASCII.GetBytes(settings.Secret);
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = settings.Issuer,
                        ValidateAudience = true,
                        ValidAudience = settings.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorizationCore(c =>
            {
                c.AddPolicy(Policies.RequireAdministratorRole, p =>
                {
                    p.RequireRole(Roles.Administrator.ToString());
                });

                c.DefaultPolicy = new AuthorizationPolicy(new List<IAuthorizationRequirement>()
                {
                    new DenyAnonymousAuthorizationRequirement()
                }, new string[]
                {
                    JwtBearerDefaults.AuthenticationScheme
                });
            });

            return services;
        }

        public static IApplicationBuilder UseDefaultRoles(this IApplicationBuilder app, IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var exist = roleManager.RoleExistsAsync(Roles.Administrator.ToString()).Result;
            if (!exist)
            {
                var role = new IdentityRole<Guid>(Roles.Administrator.ToString());
                roleManager.CreateAsync(role).Wait();
            }

            exist = roleManager.RoleExistsAsync(Roles.User.ToString()).Result;
            if (!exist)
            {
                var role = new IdentityRole<Guid>(Roles.User.ToString());
                roleManager.CreateAsync(role).Wait();
            }

            return app;
        }

        public static IApplicationBuilder UseDevUser(this IApplicationBuilder app, IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var user = userManager.FindByNameAsync("testadministrator").Result;
            if (user == null)
            {
                var result = userManager.CreateAsync(new User() { UserName = "testadministrator" }, "Test@1").Result;
                user = userManager.FindByNameAsync("testadministrator").Result;
            }

            if (!userManager.IsInRoleAsync(user, Roles.Administrator.ToString()).Result)
            {
                var result = userManager.AddToRoleAsync(user, Roles.Administrator.ToString()).Result;
            }

            user = userManager.FindByNameAsync("testuser").Result;
            if (user == null)
            {
                var result = userManager.CreateAsync(new User() { UserName = "testuser" }, "Test@1").Result;
                user = userManager.FindByNameAsync("testuser").Result;
            }

            if (!userManager.IsInRoleAsync(user, Roles.User.ToString()).Result)
            {
                var result = userManager.AddToRoleAsync(user, Roles.User.ToString()).Result;
            }
            return app;
        }
    }
}
