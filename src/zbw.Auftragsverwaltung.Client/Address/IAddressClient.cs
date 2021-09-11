using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Domain.Addresses;
using zbw.Auftragsverwaltung.Domain.Common;

namespace zbw.Auftragsverwaltung.Client.Address
{
    public interface IAddressClient
    {
        public Task<AddressDto> Get(Guid id);

        public Task<PaginatedList<AddressDto>> List(int size = 10, int page = 1, bool deleted = false);

        public Task<AddressDto> Add(AddressDto address);

        public Task<AddressDto> Update(AddressDto address);

        public Task<bool> Delete(Guid id);
    }
}
