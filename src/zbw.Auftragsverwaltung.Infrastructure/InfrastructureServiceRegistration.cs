using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Contracts;
using zbw.Auftragsverwaltung.Core.Common.Contracts;
using zbw.Auftragsverwaltung.Core.Customers.Contracts;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Infrastructure.ArticleGroups.DAL;
using zbw.Auftragsverwaltung.Infrastructure.Common.Configurations;
using zbw.Auftragsverwaltung.Infrastructure.Common.Repositories;
using zbw.Auftragsverwaltung.Infrastructure.Customers.DAL;

namespace zbw.Auftragsverwaltung.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {

        public static IServiceCollection AddInfrastructurServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<OrderManagementContext>(o =>
                o.UseSqlServer(configuration.GetConnectionString("OrderManagementConnectionString")));

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IArticleGroupRepository, ArticleGroupRepository>();
            return services;
        }

        public static IServiceCollection AddInfrastructureAuthenticationService(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<UserIdentityContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("AuthenticationConnectionString"));
            });

            services.AddIdentity<User, IdentityRole<Guid>>(o =>
            {
                o.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<UserIdentityContext>();

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
                var result = userManager.CreateAsync(new User() {UserName = "testadministrator" }, "Test@1").Result;
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
