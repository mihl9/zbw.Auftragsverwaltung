using zbw.Auftragsverwaltung.Core.Common.Contracts;
using zbw.Auftragsverwaltung.Core.Common.DTO;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Dto;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Interfaces;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using AutoMapper;

namespace zbw.Auftragsverwaltung.Core.ArticleGroups.BLL
{
    public class ArticleGroupBll : IArticleGroupBll

    {
        private readonly IMapper _mapper;
        private readonly IRepository<ArticleGroup> _articleGroupRepository;

        public ArticleGroupBll(IRepository<ArticleGroup> articleGroupRepository,IMapper mapper)
        {
            _mapper = mapper;
            _articleGroupRepository = articleGroupRepository;
        }

        public Task<ArticleGroupDto> Add(ArticleGroupDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(ArticleGroupDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ArticleGroupDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<ArticleGroupDto>> GetList(Expression<Func<ArticleGroup, bool>> predicate, int size = 10, int page = 1)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedList<ArticleGroupDto>> GetList(bool deleted = false, int size = 10, int page = 1)
        {
            throw new NotImplementedException();
        }

        public Task<ArticleGroupDto> Update(ArticleGroupDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
