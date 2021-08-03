using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using zbw.Auftragsverwaltung.Core.Customers.Contracts;
using zbw.Auftragsverwaltung.Core.Customers.Entities;

namespace zbw.Auftragsverwaltung.Core.Test.Customers
{
    public class CustomerRepositoryHelper
    {
        public static Mock<ICustomerRepository> TestCustomerRepository(IList<Customer> customers)
        {
            var repo = new Mock<ICustomerRepository>();

            repo.Setup(x => x.DeleteAsync(It.IsAny<Customer>())).ReturnsAsync(true).Callback<Customer>(x => customers.Remove(x));
            repo.Setup(x => x.UpdateAsync(It.IsAny<Customer>())).ReturnsAsync(true);
            repo.Setup(x => x.AddAsync(It.IsAny<Customer>())).ReturnsAsync((Customer c) => c).Callback<Customer>( customers.Add);
            repo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => customers.First(x => x.Id.Equals(id)));

            return repo;
        }
    }
}
