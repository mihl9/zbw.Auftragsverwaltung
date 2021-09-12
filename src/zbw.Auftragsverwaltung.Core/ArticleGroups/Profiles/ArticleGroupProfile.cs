using AutoMapper;
using zbw.Auftragsverwaltung.Core.Addresses.Entities;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Domain.Addresses;
using zbw.Auftragsverwaltung.Domain.ArticleGroups;
using zbw.Auftragsverwaltung.Domain.Common;

namespace zbw.Auftragsverwaltung.Core.ArticleGroups.Profiles
{
    public class ArticleGroupProfile : Profile
    {
        public ArticleGroupProfile()
        {
            CreateMap<ArticleGroup, ArticleGroupDto>().ReverseMap();
            CreateMap<PaginatedList<ArticleGroup>, PaginatedList<ArticleGroupDto>>();}
    }
}
