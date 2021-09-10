using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using zbw.Auftragsverwaltung.Core.Addresses.Entities;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Core.Articles.Entities;
using zbw.Auftragsverwaltung.Core.Customers.Entities;
using zbw.Auftragsverwaltung.Core.Invoices.Entities;
using zbw.Auftragsverwaltung.Core.Orders.Entities;
using zbw.Auftragsverwaltung.Core.Positions.Entities;

namespace zbw.Auftragsverwaltung.Infrastructure
{
    public class OrderManagementContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ArticleGroup> ArticleGroups { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Article> Articles { get; set; }

        public OrderManagementContext(DbContextOptions<OrderManagementContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }

    public class OrderManagementContextFactory : IDesignTimeDbContextFactory<OrderManagementContext>
    {
        public OrderManagementContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var optionsBuilder = new DbContextOptionsBuilder<OrderManagementContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("OrderManagementConnectionString"));

            return new OrderManagementContext(optionsBuilder.Options);
        }
    }

}
