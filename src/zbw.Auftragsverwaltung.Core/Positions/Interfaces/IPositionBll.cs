using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;
using zbw.Auftragsverwaltung.Core.Positions.Entities;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Domain.Positions;

namespace zbw.Auftragsverwaltung.Core.Positions.Interfaces
{
    public interface IPositionBll : ICrudBll<PositionDto, Position, Guid>
    {
    }
}
