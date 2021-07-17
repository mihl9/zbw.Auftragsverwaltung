using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using zbw.Auftragsverwaltung.Core.Common.Contracts;
using zbw.Auftragsverwaltung.Core.Customers.Contracts;
using zbw.Auftragsverwaltung.Core.Users.Entities;
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
                o.SignIn.RequireConfirmedAccount = true;
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
            return services;
        }
    }
}
