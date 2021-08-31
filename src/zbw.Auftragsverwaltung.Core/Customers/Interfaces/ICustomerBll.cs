
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;
using zbw.Auftragsverwaltung.Core.Customers.Entities;
using zbw.Auftragsverwaltung.Domain.Customers;
using zbw.Auftragsverwaltung.Domain.Users;

namespace zbw.Auftragsverwaltung.Core.Customers.Interfaces
{
    public interface ICustomerBll : ICrudAuthorizedBll<CustomerDto, Customer, Guid, Guid>
    {
        public Task<IEnumerable<CustomerDto>> GetForUser(Guid id, Guid userId);
    }
}
