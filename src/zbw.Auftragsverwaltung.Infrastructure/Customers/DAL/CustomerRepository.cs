using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.Common.DTO;
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
