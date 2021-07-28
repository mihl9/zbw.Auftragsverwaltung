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

        public async Task<ArticleGroupDto> Add(ArticleGroupDto dto)
        {
            var articleGroup = _mapper.Map<ArticleGroup>(dto);
            articleGroup = await _articleGroupRepository.AddAsync(articleGroup);

            return _mapper.Map<ArticleGroupDto>(articleGroup);
        }

        public async Task<bool> Delete(ArticleGroupDto dto)
        {
            //uc
            var articleGroup = _mapper.Map<ArticleGroup>(dto);
            await _articleGroupRepository.DeleteAsync(articleGroup);
            return true;
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
