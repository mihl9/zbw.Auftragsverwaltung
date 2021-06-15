using Microsoft.EntityFrameworkCore;
using zbw.Auftragsverwaltung.Core.Customers.Entities;

namespace zbw.Auftragsverwaltung.Infrastructure
{
    public class OrderManagementContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public OrderManagementContext(DbContextOptions<OrderManagementContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }

    

}
