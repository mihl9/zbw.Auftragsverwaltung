
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;
using zbw.Auftragsverwaltung.Core.Customers.Dto;
using zbw.Auftragsverwaltung.Core.Customers.Entities;
using zbw.Auftragsverwaltung.Core.Users.Dto;
using zbw.Auftragsverwaltung.Core.Users.Entities;

namespace zbw.Auftragsverwaltung.Core.Customers.Interfaces
{
    public interface ICustomerBll : ICrudAuthorizedBll<CustomerDto, Customer, Guid, Guid>
    {
        public Task<IEnumerable<CustomerDto>> GetForUser(UserDto user, Guid userId);
    }
}
