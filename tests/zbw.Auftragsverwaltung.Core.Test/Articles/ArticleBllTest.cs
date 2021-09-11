using System;
using System.Collections.Generic;

using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Core.Articles.BLL;
using zbw.Auftragsverwaltung.Core.Articles.Contracts;
using zbw.Auftragsverwaltung.Core.Articles.Entities;
using zbw.Auftragsverwaltung.Core.Articles.Profiles;
using zbw.Auftragsverwaltung.Core.Test.Helpers;
using zbw.Auftragsverwaltung.Domain.ArticleGroups;
using zbw.Auftragsverwaltung.Domain.Articles;

namespace zbw.Auftragsverwaltung.Core.Test.Articles
{
    public class ArticleBllTest
    {
        private readonly ArticleBll _article;
        private readonly Mock<IArticleRepository> _articleRepository;


        private readonly List<Article> _articles = new List<Article>
        {
            new Article
            {
                Id = GuidCollection.Id001,
                ArticleId = "1",
                Name = "Hammer",
                Price = 20,
                ArticleGroup = null
            },
            new Article()
            {
                Id = GuidCollection.Id002,
                ArticleId = "2",
                Name = "Schere",
                Price = 20,
                ArticleGroup = null
            },
            new Article()
            {
                Id = GuidCollection.Id003,
                ArticleId = "3",
                Name = "Säge",
                Price = 20,
                ArticleGroup = null
            }
        };



        public ArticleBllTest()
        {

            var profile = new ArticleProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            _articleRepository = ArticleRepositoryHelper.TestArticleRepository(_articles);

            _article = new ArticleBll(mapper,_articleRepository.Object);
        }

        [Fact]
        public void Get_Article_Not_Throw()
        {
            Func<Task> get = async () => { await _article.Get(GuidCollection.Id001); };
            get.Should().NotThrow<Exception>();
        }

        [Fact]
        public void Delete_Article_Not_Throw()
        {
            var articleDto = new ArticleDto
            {
                Id = GuidCollection.Id004,
                ArticleId = "2",
                Name = "Scherer",
                Price = 20,
                ArticleGroupDto = null

            };
            Func<Task> delete = async () => { await _article.Delete(articleDto); };
            delete.Should().NotThrow<Exception>();
        }

        [Fact]
        public void Add_Article_Not_Throw_And_Not_Null()
        {
            var articleDto = new ArticleDto
            {
                Id = GuidCollection.Id004,
                ArticleId = "5",
                Name = "Schraubenzieher",
                Price = 50,
                ArticleGroupDto = null

            };


            Func<Task> add = async () => { await _article.Add(articleDto); };
            add.Should().NotThrow<Exception>();
            Func<Task> get = async () => { await _article.Get(GuidCollection.Id006); };
            get.Should().NotBeNull();
        }

        [Fact]
        public void Update_Article_Not_Throw_And_Not_Null()
        {
            var ArticleDto = new ArticleDto
            {
                Id = GuidCollection.Id003,
                ArticleId = "3",
                Name = "Säge",
                Price = 100,
                ArticleGroupDto = null
            };
            Func<Task> update = async () => { await _article.Update(ArticleDto); };
            update.Should().NotThrow<Exception>();
            update.Should().NotBeNull();
        }
    }
}
