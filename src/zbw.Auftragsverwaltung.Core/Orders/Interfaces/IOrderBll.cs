using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;
using zbw.Auftragsverwaltung.Core.Orders.Dto;
using zbw.Auftragsverwaltung.Core.Orders.Entities;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using System.Threading.Tasks;

namespace zbw.Auftragsverwaltung.Core.Orders.Interfaces
{
    public interface IOrderBll : ICrudBll<OrderDto, Order, Guid>
    {
        public Task<IEnumerable<OrderDto>> GetForUser(User user);
    }
}
