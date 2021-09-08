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
        private const string CTE_SQL_COMMAND = "WITH CTE_ARTICLEGROUPS (Id, ArticleGroupName, ArticleGroupId ) AS (SELECT Id, Name, ArticlegroupId FROM dbo.ArticleGroups WHERE ArticlegroupId IS NULL UNION ALL SELECT pn.Id,pn.Name, pn.ArticlegroupId FROM dbo.ArticleGroups AS pn INNER JOIN CTE_ARTICLEGROUPS AS p1 ON p1.Id = pn.ArticlegroupId) SELECT	Id,ArticleGroupName, ArticlegroupId FROM CTE_ARTICLEGROUPS ORDER BY ArticlegroupId";

        public ArticleGroupRepository(OrderManagementContext dbContext) : base(dbContext)
        {
        }
        public async Task<PaginatedList<ArticleGroup>> GetCTERecursive(int size, int page)
        {
            return PaginatedList<ArticleGroup>.ToPagedResult(await _dbContext.ArticleGroups.FromSqlRaw(CTE_SQL_COMMAND).ToListAsync(),page,size);
        }
    }
}
