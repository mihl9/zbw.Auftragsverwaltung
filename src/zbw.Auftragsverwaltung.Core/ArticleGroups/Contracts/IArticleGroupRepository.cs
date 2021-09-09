
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Core.Common.Contracts;
using zbw.Auftragsverwaltung.Domain.Common;

namespace zbw.Auftragsverwaltung.Core.ArticleGroups.Contracts
{
    public interface IArticleGroupRepository : IRepository<ArticleGroup, Guid>
    {
        Task<IReadOnlyList<ArticleGroup>> GetCTERecursive();
    }
}
