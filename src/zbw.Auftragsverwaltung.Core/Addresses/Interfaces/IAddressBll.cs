using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.Addresses.Entities;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;
using zbw.Auftragsverwaltung.Domain.Addresses;

namespace zbw.Auftragsverwaltung.Core.Addresses.Interfaces
{
    public interface IAddressBll : ICrudAuthorizedBll<AddressDto, Address, Guid, Guid>
    {
    }
}
