using Microsoft.EntityFrameworkCore;
using zbw.Auftragsverwaltung.Core.Customers.Entities;
using zbw.Auftragsverwaltung.Core.Orders.Entities;

namespace zbw.Auftragsverwaltung.Infrastructure
{
    public class OrderManagementContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public OrderManagementContext(DbContextOptions<OrderManagementContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }

    

}
