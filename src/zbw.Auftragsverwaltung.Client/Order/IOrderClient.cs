using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Orders;

namespace zbw.Auftragsverwaltung.Client.Order
{
    public interface IOrderClient
    {
        public Task<OrderDto> Get(Guid id);

        public Task<PaginatedList<OrderDto>> List(int size = 10, int page = 1, bool deleted = false);

        public Task<OrderDto> Add(OrderDto order);

        public Task<OrderDto> Update(OrderDto order);

        public Task<bool> Delete(Guid id);
    }
}
