using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Contracts;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Core.Articles.Contracts;
using zbw.Auftragsverwaltung.Core.Articles.Entities;

namespace zbw.Auftragsverwaltung.Core.Test.Articles
{
    public class ArticleRepositoryHelper
    {
        public static Mock<IArticleRepository> TestArticleRepository(IList<Article> articles)
        {
            var repo = new Mock<IArticleRepository>();

            repo.Setup(x => x.DeleteAsync(It.IsAny<Article>())).ReturnsAsync(true).Callback<Article>(x => articles.Remove(x));
            repo.Setup(x => x.UpdateAsync(It.IsAny<Article>())).ReturnsAsync(true);
            repo.Setup(x => x.AddAsync(It.IsAny<Article>())).ReturnsAsync((Article c) => c).Callback<Article>( articles.Add);
            repo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => articles.First(x => x.Id.Equals(id)));

            return repo;
        }
    }
}
