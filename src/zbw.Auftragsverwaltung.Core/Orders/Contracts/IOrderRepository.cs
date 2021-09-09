using System;
using zbw.Auftragsverwaltung.Core.Common.Contracts;
using zbw.Auftragsverwaltung.Core.Orders.Entities;

namespace zbw.Auftragsverwaltung.Core.Orders.Contracts
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {

    }
}
