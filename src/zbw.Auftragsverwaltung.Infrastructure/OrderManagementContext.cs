using Microsoft.EntityFrameworkCore;
using zbw.Auftragsverwaltung.Core.Addresses.Entities;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Core.Customers.Entities;
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

        public OrderManagementContext(DbContextOptions<OrderManagementContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }

    

}
