using System;
using zbw.Auftragsverwaltung.Core.Customers.Contracts;
using zbw.Auftragsverwaltung.Core.Customers.Entities;
using zbw.Auftragsverwaltung.Infrastructure.Common.Repositories;

namespace zbw.Auftragsverwaltung.Infrastructure.Customers.DAL
{
    public class CustomerRepository : BaseRepository<Customer, Guid, OrderManagementContext>, ICustomerRepository
    {
        public CustomerRepository(OrderManagementContext dbContext) : base(dbContext)
        {
        }
    }
}
