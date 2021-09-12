using System.IO;
using System.Threading;
using System.Threading.Tasks;
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
using zbw.Auftragsverwaltung.Infrastructure.Common;
using zbw.Auftragsverwaltung.Infrastructure.Common.Helpers;

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
            modelBuilder.Entity<Address>().HasKey(x => new { x.Id, x.ValidFrom });
            modelBuilder.Entity<Invoice>(x =>
            {
                x.HasOne(x => x.Address).WithMany().HasForeignKey(f=>new {f.AddressId,f.AdressValidFrom});
            });
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            await EntityHistoryHelper.CreateHistory(this);
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            await EntityHistoryHelper.CreateHistory(this);
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            EntityHistoryHelper.CreateHistory(this).Wait();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            EntityHistoryHelper.CreateHistory(this).Wait();
            return base.SaveChanges(acceptAllChangesOnSuccess);
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
