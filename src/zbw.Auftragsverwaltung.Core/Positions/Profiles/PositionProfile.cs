using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using zbw.Auftragsverwaltung.Core.Common.DTO;
using zbw.Auftragsverwaltung.Core.Positions.Dto;
using zbw.Auftragsverwaltung.Core.Positions.Entities;

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
