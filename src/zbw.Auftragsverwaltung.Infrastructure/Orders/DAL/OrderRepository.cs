using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.Common.DTO;
using zbw.Auftragsverwaltung.Infrastructure.Common.Repositories;
using zbw.Auftragsverwaltung.Core.Orders.Entities;
using zbw.Auftragsverwaltung.Core.Orders.Contracts;

namespace zbw.Auftragsverwaltung.Infrastructure.Orders.DAL
{
    public class OrderRepository : BaseRepository<Order, Guid, OrderManagementContext>, IOrderRepository
    {
        public OrderRepository(OrderManagementContext dbContext) : base(dbContext)
        {
        }
    }
}
