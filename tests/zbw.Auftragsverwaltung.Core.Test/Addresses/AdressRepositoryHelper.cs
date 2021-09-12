using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using zbw.Auftragsverwaltung.Core.Addresses.Contracts;
using zbw.Auftragsverwaltung.Core.Addresses.Entities;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Contracts;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;

namespace zbw.Auftragsverwaltung.Core.Test.Addresses
{
    public class AdressRepositoryHelper
    {
        public static Mock<IAddressRepository> TestAdressRepository(IList<Address> address)
        {
            var repo = new Mock<IAddressRepository>();

            repo.Setup(x => x.DeleteAsync(It.IsAny<Address>())).ReturnsAsync(true).Callback<Address>(x => address.Remove(x));
            repo.Setup(x => x.UpdateAsync(It.IsAny<Address>())).ReturnsAsync(true);
            repo.Setup(x => x.AddAsync(It.IsAny<Address>())).ReturnsAsync((Address c) => c).Callback<Address>( address.Add);
            repo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => address.First(x => x.Id.Equals(id)));

            return repo;
        }
    }
}
