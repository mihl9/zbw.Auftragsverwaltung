using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Domain.Articles;
using zbw.Auftragsverwaltung.Domain.Common;

namespace zbw.Auftragsverwaltung.Client.Article
{
    public interface IArticleClient
    {
        public Task<ArticleDto> Get(Guid id);

        public Task<PaginatedList<ArticleDto>> List(int size = 10, int page = 1, bool deleted = false);

        public Task<ArticleDto> Add(ArticleDto article);

        public Task<ArticleDto> Update(ArticleDto article);

        public Task<bool> Delete(Guid id);
    }
}
