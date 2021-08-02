
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Core.Common.Contracts;
using zbw.Auftragsverwaltung.Core.Common.DTO;

namespace zbw.Auftragsverwaltung.Core.ArticleGroups.Contracts
{
    public interface IArticleGroupRepository : IRepository<ArticleGroup, Guid>
    {
    }
}
