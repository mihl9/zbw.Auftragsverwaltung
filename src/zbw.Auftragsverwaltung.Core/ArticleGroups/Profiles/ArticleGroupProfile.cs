using AutoMapper;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Domain.ArticleGroups;

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
