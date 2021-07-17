using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;

namespace zbw.Auftragsverwaltung.Infrastructure.Migrators
{
    public static class OrderManagementMigrator
    {
        public static IApplicationBuilder MigrateOrderDatabase(this IApplicationBuilder builder)
        {
            using var scope = builder.ApplicationServices.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<OrderManagementContext>();

            //var sp = (ctx as IInfrastructure<IServiceProvider>).Instance;

            //var migrationAssembly = sp.GetRequiredService<IMigrationsAssembly>();
            //var differ = sp.GetRequiredService<IMigrationsModelDiffer>();

            //var snap = migrationAssembly.ModelSnapshot.Model;
            
            //if (differ.GetDifferences(snap.GetRelationalModel(), ctx.Model.GetRelationalModel()).Any())
            //{
            //    throw new InvalidOperationException("There are differences between the current database and the most recent migration");
            //}

            ctx.Database.Migrate();

            return builder;
        }
    }
}
