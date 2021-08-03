using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Interfaces;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;
using AutoMapper;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Contracts;
using zbw.Auftragsverwaltung.Domain.ArticleGroups;
using zbw.Auftragsverwaltung.Domain.Common;

namespace zbw.Auftragsverwaltung.Core.ArticleGroups.BLL
{
    public class ArticleGroupBll : IArticleGroupBll

    {
        private readonly IMapper _mapper;
        private readonly IArticleGroupRepository _articleGroupRepository;

        public ArticleGroupBll(IArticleGroupRepository articleGroupRepository,IMapper mapper)
        {
            _mapper = mapper;
            _articleGroupRepository = articleGroupRepository;
        }

        public async Task<ArticleGroupDto> Add(ArticleGroupDto dto)
        {
            dto.Id = new Guid();
            var articleGroup = _mapper.Map<ArticleGroup>(dto);
            articleGroup = await _articleGroupRepository.AddAsync(articleGroup);

            return _mapper.Map<ArticleGroupDto>(articleGroup);
        }

        public async Task<bool> Delete(ArticleGroupDto dto)
        {
            var articleGroup = _mapper.Map<ArticleGroup>(dto);
            return await _articleGroupRepository.UpdateAsync(articleGroup);
        }

        public async Task<ArticleGroupDto> Get(Guid id)
        {
            var articleGroup = await _articleGroupRepository.GetByIdAsync(id);
            return _mapper.Map<ArticleGroupDto>(articleGroup);
        }

        public async Task<PaginatedList<ArticleGroupDto>> GetList(Expression<Func<ArticleGroup, bool>> predicate, int size = 10, int page = 1)
        {
            var articleGroups = await _articleGroupRepository.GetPagedResponseAsync(page, size, predicate);

            return _mapper.Map<PaginatedList<ArticleGroupDto>>(articleGroups);
        }

        public async Task<PaginatedList<ArticleGroupDto>> GetList(bool deleted = false, int size = 10, int page = 1)
        {
            var articleGroups = await _articleGroupRepository.GetPagedResponseAsync(page, size);

            return _mapper.Map<PaginatedList<ArticleGroupDto>>(articleGroups);
        }

        public async Task<ArticleGroupDto> Update(ArticleGroupDto dto)
        {
            var articleGroup = _mapper.Map<ArticleGroup>(dto);
            await _articleGroupRepository.UpdateAsync(articleGroup);

            return _mapper.Map<ArticleGroupDto>(articleGroup);
        }
    }
}
