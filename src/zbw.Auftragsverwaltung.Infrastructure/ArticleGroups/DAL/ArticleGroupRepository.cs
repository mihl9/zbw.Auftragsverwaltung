using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Contracts;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Infrastructure.Common.Repositories;

namespace zbw.Auftragsverwaltung.Infrastructure.ArticleGroups.DAL
{
    public class ArticleGroupRepository : BaseRepository<ArticleGroup, Guid, OrderManagementContext>,IArticleGroupRepository
    {
        public ArticleGroupRepository(OrderManagementContext dbContext) : base(dbContext)
        {
        }
    }
}
