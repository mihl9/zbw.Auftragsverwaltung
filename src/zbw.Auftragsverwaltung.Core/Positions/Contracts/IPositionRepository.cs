using System;
using zbw.Auftragsverwaltung.Core.Common.Contracts;
using zbw.Auftragsverwaltung.Core.Positions.Entities;

namespace zbw.Auftragsverwaltung.Core.Positions.Contracts
{
    public interface IPositionRepository : IRepository<Position, Guid>
    {

    }
}
