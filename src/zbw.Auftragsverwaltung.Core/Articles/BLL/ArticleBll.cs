using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using zbw.Auftragsverwaltung.Core.Articles.Contracts;
using zbw.Auftragsverwaltung.Core.Articles.Dto;
using zbw.Auftragsverwaltung.Core.Articles.Entities;
using zbw.Auftragsverwaltung.Core.Articles.Interfaces;
using zbw.Auftragsverwaltung.Domain.Common;

namespace zbw.Auftragsverwaltung.Core.Articles.BLL
{
    public class ArticleBll: IArticleBll
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepository;

        public ArticleBll(IMapper mapper, IArticleRepository articleRepository)
        {
            _mapper = mapper;
            _articleRepository = articleRepository;
        }

        public async Task<ArticleDto> Get(Guid id)
        {
            var article = await _articleRepository.GetByIdAsync(id);
            return _mapper.Map<ArticleDto>(article);
        }

        public async Task<PaginatedList<ArticleDto>> GetList(Expression<Func<Article, bool>> predicate, int size = 10, int page = 1)
        {
            var articles = await _articleRepository.GetPagedResponseAsync(page, size, predicate);
            return _mapper.Map<PaginatedList<ArticleDto>>(articles);
        }

        public async Task<PaginatedList<ArticleDto>> GetList(bool deleted = false, int size = 10, int page = 1)
        {
            var articles = await _articleRepository.GetPagedResponseAsync(page, size);
            return _mapper.Map<PaginatedList<ArticleDto>>(articles);
        }

        public async Task<ArticleDto> Add(ArticleDto dto)
        {
            dto.Id = new Guid();
            var article = _mapper.Map<Article>(dto);
            article = await _articleRepository.AddAsync(article);

            return _mapper.Map<ArticleDto>(article);
        }

        public async Task<bool> Delete(ArticleDto dto)
        {

            var article = _mapper.Map<Article>(dto);
            return await _articleRepository.UpdateAsync(article);

        }

        public async Task<ArticleDto> Update(ArticleDto dto)
        {
            var article = _mapper.Map<Article>(dto);
            await _articleRepository.DeleteAsync(article);

            return _mapper.Map<ArticleDto>(article);
        }
    }
}
