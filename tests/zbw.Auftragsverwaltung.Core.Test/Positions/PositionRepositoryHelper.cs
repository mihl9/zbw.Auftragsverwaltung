using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using zbw.Auftragsverwaltung.Core.Positions.Contracts;
using zbw.Auftragsverwaltung.Core.Positions.Entities;

namespace zbw.Auftragsverwaltung.Core.Test.Positions
{
    class PositionRepositoryHelper
    {
        public static Mock<IPositionRepository> TestPositionRepository(IList<Position> positions)
        {
            var repo = new Mock<IPositionRepository>();

            repo.Setup(x => x.DeleteAsync(It.IsAny<Position>())).ReturnsAsync(true).Callback<Position>(x => positions.Remove(x));
            repo.Setup(x => x.UpdateAsync(It.IsAny<Position>())).ReturnsAsync(true);
            repo.Setup(x => x.AddAsync(It.IsAny<Position>())).ReturnsAsync((Position c) => c).Callback<Position>(positions.Add);
            repo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => positions.First(x => x.Id.Equals(id)));

            return repo;
        }
    }
}
