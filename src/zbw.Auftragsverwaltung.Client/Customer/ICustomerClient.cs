using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Customers;

namespace zbw.Auftragsverwaltung.Client.Customer
{
    public interface ICustomerClient
    {

        public Task<CustomerDto> Get(Guid id);

        public Task<PaginatedList<CustomerDto>> List(int size = 10, int page = 1, bool deleted = false);

        public Task<CustomerDto> Add(CustomerDto customer);

        public Task<CustomerDto> Update(CustomerDto customer);

        public Task<bool> Delete(Guid id);
    }
}
