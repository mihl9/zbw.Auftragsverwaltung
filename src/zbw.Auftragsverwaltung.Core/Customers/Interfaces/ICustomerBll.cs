
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.Customers.Dto;

namespace zbw.Auftragsverwaltung.Core.Customers.Interfaces
{
    public interface ICustomerBll
    {
        public Task<CustomerDto> Get(Guid id);
    }
}
