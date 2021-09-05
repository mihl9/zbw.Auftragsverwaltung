using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.Addresses.Contracts;
using zbw.Auftragsverwaltung.Core.Addresses.Entities;
using zbw.Auftragsverwaltung.Infrastructure.Common.Repositories;

namespace zbw.Auftragsverwaltung.Infrastructure.Addresses.DAL
{
    public class AddressRepository : BaseRepository<Address, Guid, OrderManagementContext>, IAddressRepository
    {
        public AddressRepository(OrderManagementContext dbContext) : base(dbContext)
        {
        }
    }
}
