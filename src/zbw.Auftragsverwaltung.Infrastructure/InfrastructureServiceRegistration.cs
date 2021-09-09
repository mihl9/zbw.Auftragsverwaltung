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
using zbw.Auftragsverwaltung.Core.Addresses.Contracts;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Contracts;
using zbw.Auftragsverwaltung.Core.Articles.Contracts;
using zbw.Auftragsverwaltung.Core.Common.Contracts;
using zbw.Auftragsverwaltung.Core.Customers.Contracts;
using zbw.Auftragsverwaltung.Core.Orders.Contracts;
using zbw.Auftragsverwaltung.Core.Positions.Contracts;
using zbw.Auftragsverwaltung.Core.Users.Contracts;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Infrastructure.Addresses.DAL;
using zbw.Auftragsverwaltung.Infrastructure.ArticleGroups.DAL;
using zbw.Auftragsverwaltung.Infrastructure.Articles.DAL;
using zbw.Auftragsverwaltung.Infrastructure.Positions.DAL;
using zbw.Auftragsverwaltung.Infrastructure.Common.Repositories;
using zbw.Auftragsverwaltung.Infrastructure.Customers.DAL;
using zbw.Auftragsverwaltung.Infrastructure.Orders.DAL;
using zbw.Auftragsverwaltung.Infrastructure.Users.DAL;

namespace zbw.Auftragsverwaltung.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {

        public static IServiceCollection AddInfrastructurServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<OrderManagementContext>(o =>
                o.UseSqlServer(configuration.GetConnectionString("OrderManagementConnectionString")));
            
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<IAddressRepository, AddressRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IArticleGroupRepository, ArticleGroupRepository>();
            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IPositionRepository, PositionRepository>();

            services.AddDbContext<UserIdentityContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("AuthenticationConnectionString"));
            });

            services.AddIdentity<User, IdentityRole<Guid>>(o =>
            {
                o.SignIn.RequireConfirmedAccount = false;
            }).AddEntityFrameworkStores<UserIdentityContext>();

            return services;
        }

        

    }
}
