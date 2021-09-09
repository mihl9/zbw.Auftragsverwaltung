using System;
using zbw.Auftragsverwaltung.Infrastructure.Common.Repositories;
using zbw.Auftragsverwaltung.Core.Positions.Entities;
using zbw.Auftragsverwaltung.Core.Positions.Contracts;

namespace zbw.Auftragsverwaltung.Infrastructure.Positions.DAL
{
    public class PositionRepository : BaseRepository<Position, Guid, OrderManagementContext>, IPositionRepository
    {
        public PositionRepository(OrderManagementContext dbContext) : base(dbContext)
        {
        }
    }
}
