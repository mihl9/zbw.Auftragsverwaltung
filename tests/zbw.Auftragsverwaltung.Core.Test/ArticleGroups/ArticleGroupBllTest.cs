using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;
using zbw.Auftragsverwaltung.Core.ArticleGroups.BLL;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Contracts;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Profiles;
using zbw.Auftragsverwaltung.Core.Customers.Contracts;
using zbw.Auftragsverwaltung.Core.Customers.Profiles;
using zbw.Auftragsverwaltung.Core.Test.Helpers;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Domain.ArticleGroups;

namespace zbw.Auftragsverwaltung.Core.Test.ArticleGroups
{
    public class ArticleGroupBllTest
    {
        private readonly ArticleGroupBll _articleGroup;
        private readonly Mock<IArticleGroupRepository> _articleGroupRepository;

        private readonly List<ArticleGroup> _articleGroups = new List<ArticleGroup>();


        private static ArticleGroup _articleGroup1 = new ArticleGroup()
        {
            Id = GuidCollection.Id010,
            Name = "Eisenwaren",
            ParentId = null
        };
        private static ArticleGroup _articleGroup2 = new ArticleGroup()
        {
            Id = GuidCollection.Id009,
            Name = "Nägel",
            ParentId = _articleGroup1.Id
        };
        private static ArticleGroup _articleGroup3 = new ArticleGroup()
        {
            Id = GuidCollection.Id008,
            Name = "Schrauben",
            ParentId = _articleGroup1.Id
        };
        private static ArticleGroup _articleGroup4 = new ArticleGroup()
        {
            Id = GuidCollection.Id007,
            Name = "Holzwaren",
            ParentId = null
        };


        public ArticleGroupBllTest()
        {
            _articleGroups.Add(_articleGroup1);
            _articleGroups.Add(_articleGroup2);
            _articleGroups.Add(_articleGroup3);
            _articleGroups.Add(_articleGroup4);

            var profile = new ArticleGroupProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            _articleGroupRepository = ArticleGroupRepositoryHelper.TestArticleGroupRepoistory(_articleGroups);

            _articleGroup = new ArticleGroupBll(_articleGroupRepository.Object, mapper);
        }

        [Fact]
        public void Get_ArticleGroup_Not_Throw()
        {
            Func<Task> get = async () => { await _articleGroup.Get(GuidCollection.Id010); };
            get.Should().NotThrow<Exception>();
        }

        [Fact]
        public void Delete_ArticleGroup_Not_Throw()
        {
            var articleGroupDto = new ArticleGroupDto
            {
                Id = GuidCollection.Id007,
                Name = "Holzwaren"
            };
            Func<Task> delete = async () => { await _articleGroup.Delete(articleGroupDto); };
            delete.Should().NotThrow<Exception>();
        }

        [Fact]
        public void Add_ArticleGroup_Not_Throw_And_Not_Null()
        {
            var articleGroupDto = new ArticleGroupDto
            {
                Id = GuidCollection.Id005,
                Name = "Bretter",
                ParentId = GuidCollection.Id007
            };
            Func<Task> add = async () => { await _articleGroup.Add(articleGroupDto); };
            add.Should().NotThrow<Exception>();
            Func<Task> get = async () => { await _articleGroup.Get(GuidCollection.Id006); };
            get.Should().NotBeNull();
        }

        [Fact]
        public void Update_ArticleGroup_Not_Throw_And_Not_Null()
        {
            var articleGroupDto = new ArticleGroupDto
            {
                Id = GuidCollection.Id005,
                Name = "BretterBlubb",
                ParentId = GuidCollection.Id007
            };
            Func<Task> update = async () => { await _articleGroup.Update(articleGroupDto); };
            update.Should().NotThrow<Exception>();
            update.Should().NotBeNull();
        }
    }
}
