using System;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.Addresses.Contracts;
using zbw.Auftragsverwaltung.Core.Addresses.Entities;
using zbw.Auftragsverwaltung.Core.Test.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace zbw.Auftragsverwaltung.Core.Test.DataTransfer
{
    public class AddressRepositoryHelper
    {
        public static Mock<IAddressRepository> TestAddressRepository(IList<Address> address)
        {
            var repo = new Mock<IAddressRepository>();

            repo.Setup(x => x.DeleteAsync(It.IsAny<Address>())).ReturnsAsync(true).Callback<Address>(x => address.Remove(x));
            repo.Setup(x => x.UpdateAsync(It.IsAny<Address>())).ReturnsAsync(true);
            repo.Setup(x => x.AddAsync(It.IsAny<Address>())).ReturnsAsync((Address c) => c).Callback<Address>(address.Add);
            repo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => address.First(x => x.Id.Equals(id)));
            repo.Setup(x => x.ListAsync(false)).Returns(GetListAsync(address));

            return repo;
        }

        private static async Task<IReadOnlyList<Address>> GetListAsync(IList<Address> address)
        {
            List<Address> adr = new List<Address>(address);
            IReadOnlyList<Address> list = adr.AsReadOnly();
            IReadOnlyList<Address> task = await Task.Run(() => list);
            return task;
        }
    }
}
