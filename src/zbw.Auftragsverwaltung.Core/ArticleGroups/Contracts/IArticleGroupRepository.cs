
using System;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Core.Common.Contracts;

namespace zbw.Auftragsverwaltung.Core.ArticleGroups.Contracts
{
    public interface IArticleGroupRepository : IRepository<ArticleGroup, Guid>
    {
    }
}
