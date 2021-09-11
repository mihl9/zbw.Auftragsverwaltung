using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Domain.Positions;

namespace zbw.Auftragsverwaltung.Client.Positions
{
    public interface IPositionClient
    {
        public Task<PositionDto> Get(Guid id);

        public Task<PaginatedList<PositionDto>> List(int size = 10, int page = 1, bool deleted = false);

        public Task<PositionDto> Add(PositionDto position);

        public Task<PositionDto> Update(PositionDto position);

        public Task<bool> Delete(Guid id);
    }
}
