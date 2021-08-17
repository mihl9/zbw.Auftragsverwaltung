using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Domain.Customers;

namespace zbw.Auftragsverwaltung.Client.Customer
{
    public interface ICustomerClient
    {

        public Task<CustomerDto> Get(Guid id);
    }
}
