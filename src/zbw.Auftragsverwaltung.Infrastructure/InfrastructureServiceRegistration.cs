using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using zbw.Auftragsverwaltung.Core.Contracts.Infrastructure;
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
    }
}
