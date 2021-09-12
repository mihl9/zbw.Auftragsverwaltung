using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Contracts;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Domain.Common;
using zbw.Auftragsverwaltung.Infrastructure.Common.Repositories;

namespace zbw.Auftragsverwaltung.Infrastructure.ArticleGroups.DAL
{
    public class ArticleGroupRepository : BaseRepository<ArticleGroup, Guid, OrderManagementContext>, IArticleGroupRepository
    {
        private const string CTE_SQL_COMMAND = "WITH CTE_ARTICLEGROUPS (Id, Name, ParentId ) AS (SELECT Id, Name, ParentId FROM dbo.ArticleGroups WHERE ParentId IS NULL UNION ALL SELECT pn.Id,pn.Name, pn.ParentId FROM dbo.ArticleGroups AS pn INNER JOIN CTE_ARTICLEGROUPS AS p1 ON p1.Id = pn.ParentId) SELECT	Id,Name, ParentId FROM CTE_ARTICLEGROUPS ORDER BY ParentId";

        public ArticleGroupRepository(OrderManagementContext dbContext) : base(dbContext)
        {
        }
        public async Task<IReadOnlyList<ArticleGroup>> GetCTERecursive()
        {
            return await _dbContext.ArticleGroups.FromSqlRaw(CTE_SQL_COMMAND).ToListAsync();
        }
    }
}
