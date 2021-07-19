using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using zbw.Auftragsverwaltung.Core.Customers.BLL;
using zbw.Auftragsverwaltung.Core.Customers.Interfaces;
using zbw.Auftragsverwaltung.Core.Users.Bll;
using zbw.Auftragsverwaltung.Core.Users.Interfaces;

namespace zbw.Auftragsverwaltung.Core
{
    public static class CoreRegistrationService
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<ICustomerBll, CustomerBll>();
            services.AddScoped<IUserBll, UserBll>();

            return services;
        }

    }
}
