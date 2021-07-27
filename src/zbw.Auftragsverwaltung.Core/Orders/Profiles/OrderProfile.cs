using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using zbw.Auftragsverwaltung.Core.Common.DTO;
using zbw.Auftragsverwaltung.Core.Orders.Dto;
using zbw.Auftragsverwaltung.Core.Orders.Entities;

namespace zbw.Auftragsverwaltung.Core.Orders.Profiles
{
    class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<PaginatedList<Order>, PaginatedList<OrderDto>>();
        }
    }
}
