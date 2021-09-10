using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Contracts;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;

namespace zbw.Auftragsverwaltung.Core.Test.ArticleGroups
{
    public class ArticleGroupRepositoryHelper
    {
        public static Mock<IArticleGroupRepository> TestArticleGroupRepoistory(IList<ArticleGroup> articleGroups)
        {
            var repo = new Mock<IArticleGroupRepository>();

            repo.Setup(x => x.DeleteAsync(It.IsAny<ArticleGroup>())).ReturnsAsync(true).Callback<ArticleGroup>(x => articleGroups.Remove(x));
            repo.Setup(x => x.UpdateAsync(It.IsAny<ArticleGroup>())).ReturnsAsync(true);
            repo.Setup(x => x.AddAsync(It.IsAny<ArticleGroup>())).ReturnsAsync((ArticleGroup c) => c).Callback<ArticleGroup>( articleGroups.Add);
            repo.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Guid id) => articleGroups.First(x => x.Id.Equals(id)));

            return repo;
        }
    }
}
