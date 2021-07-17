
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;
using zbw.Auftragsverwaltung.Core.Customers.Dto;
using zbw.Auftragsverwaltung.Core.Customers.Entities;
using zbw.Auftragsverwaltung.Core.Users.Entities;

namespace zbw.Auftragsverwaltung.Core.Customers.Interfaces
{
    public interface ICustomerBll : ICrudBll<CustomerDto, Customer, Guid>
    {
        public Task<IEnumerable<CustomerDto>> GetForUser(User user);
    }
}
