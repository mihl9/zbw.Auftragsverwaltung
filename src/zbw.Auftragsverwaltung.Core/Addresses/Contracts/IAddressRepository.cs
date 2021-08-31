using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.Addresses.Entities;
using zbw.Auftragsverwaltung.Core.Common.Contracts;

namespace zbw.Auftragsverwaltung.Core.Addresses.Contracts
{
    public interface IAddressRepository : IRepository<Address, Guid>
    {
    }
}
