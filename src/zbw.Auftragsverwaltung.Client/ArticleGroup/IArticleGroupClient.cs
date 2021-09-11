using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Domain.ArticleGroups;
using zbw.Auftragsverwaltung.Domain.Common;

namespace zbw.Auftragsverwaltung.Client.ArticleGroup
{
    public interface IArticleGroupClient
    {
        public Task<ArticleGroupDto> Get(Guid id);

        public Task<PaginatedList<ArticleGroupDto>> List(int size = 10, int page = 1, bool deleted = false);

        public Task<ArticleGroupDto> Add(ArticleGroupDto articleGroup);

        public Task<ArticleGroupDto> Update(ArticleGroupDto articleGroup);

        public Task<bool> Delete(Guid id);
    }
}
