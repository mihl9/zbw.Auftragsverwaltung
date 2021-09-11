using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Core.Positions.Entities;
using zbw.Auftragsverwaltung.Domain.Positions;

namespace zbw.Auftragsverwaltung.Core.Positions.Profiles
{
    public class PositionProfile : Profile
    {
        public PositionProfile()
        {
            CreateMap<Position, PositionDto>().ReverseMap();
            CreateMap<PaginatedList<Position>, PaginatedList<PositionDto>>();
        }
    }
}
