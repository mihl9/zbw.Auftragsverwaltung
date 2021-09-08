using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using zbw.Auftragsverwaltung.Core.Orders.Contracts;
using zbw.Auftragsverwaltung.Core.Orders.Entities;

namespace zbw.Auftragsverwaltung.Core.Test.Orders
{
    class OrderRepositoryHelper
    {
        public static Mock<IOrderRepository> TestOrderRepository(IList<Order> orders)
        {
            var repo = new Mock<IOrderRepository>();

            repo.Setup(x => x.DeleteAsync(It.IsAny<Order>())).ReturnsAsync(true).Callback<Order>(x => orders.Remove(x));
            repo.Setup(x => x.UpdateAsync(It.IsAny<Order>())).ReturnsAsync(true);
            repo.Setup(x => x.AddAsync(It.IsAny<Order>())).ReturnsAsync((Order c) => c).Callback<Order>(orders.Add);
            repo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => orders.First(x => x.Id.Equals(id)));

            return repo;
        }
    }
}
