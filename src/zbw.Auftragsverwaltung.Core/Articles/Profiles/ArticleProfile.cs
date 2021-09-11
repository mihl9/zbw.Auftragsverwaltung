using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using zbw.Auftragsverwaltung.Core.Articles.Entities;
using zbw.Auftragsverwaltung.Domain.Articles;
using zbw.Auftragsverwaltung.Domain.Common;

namespace zbw.Auftragsverwaltung.Core.Articles.Profiles
{
    public class ArticleProfile : Profile
    {
        public ArticleProfile()
        {
            CreateMap<Article, ArticleDto>().ReverseMap();
            CreateMap<PaginatedList<Article>, PaginatedList<ArticleDto>>();
        }
    }
}
