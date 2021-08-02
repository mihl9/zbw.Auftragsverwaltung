using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Dto;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;

namespace zbw.Auftragsverwaltung.Core.ArticleGroups.Profiles
{
    public class ArticleGroupProfile : Profile
    {
        public ArticleGroupProfile()
        {
            CreateMap<ArticleGroup, ArticleGroupDto>().ReverseMap();
        }
    }
}
