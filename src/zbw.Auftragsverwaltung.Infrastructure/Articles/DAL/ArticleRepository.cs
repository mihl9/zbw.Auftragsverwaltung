using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.Articles.Contracts;
using zbw.Auftragsverwaltung.Core.Articles.Entities;
using zbw.Auftragsverwaltung.Infrastructure.Common.Repositories;

namespace zbw.Auftragsverwaltung.Infrastructure.Articles.DAL
{
    public class ArticleRepository : BaseRepository<Article,Guid,OrderManagementContext>,IArticleRepository
    {
        public ArticleRepository(OrderManagementContext dbContext) : base(dbContext)
        {
        }
    }
}
