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
using zbw.Auftragsverwaltung.Core.Common.Exceptions;
using zbw.Auftragsverwaltung.Core.Positions.BLL;
using zbw.Auftragsverwaltung.Core.Positions.Contracts;
using zbw.Auftragsverwaltung.Core.Positions.Entities;
using zbw.Auftragsverwaltung.Core.Positions.Profiles;
using zbw.Auftragsverwaltung.Core.Test.Helpers;
using zbw.Auftragsverwaltung.Core.Test.Users;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Core.Articles.Entities;
using zbw.Auftragsverwaltung.Core.Articles.Dto;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Domain.ArticleGroups;
using zbw.Auftragsverwaltung.Domain.Positions;

namespace zbw.Auftragsverwaltung.Core.Test.Positions
{
    public class PositionBllTest
    {
        private readonly PositionBll _position;
        private readonly Mock<IPositionRepository> _positionRepository;
        private readonly Mock<UserManager<User>> _userManager;

        private readonly List<User> _users = new List<User>()
        {
            new User()
            {
                UserName = "Administrator", Email = "admin@admin.ch",
                Id = GuidCollection.Id001
            },
            new User()
            {
                UserName = "User", Email = "user@user.ch",
                Id = GuidCollection.Id002
            }
        };

        private readonly List<Position> _positions = new List<Position>()
        {
           new Position() {
            Amount = 3, Nr = 1, Id = GuidCollection.Id001, 
            Article = new Article() { Id = GuidCollection.Id003, ArticleId = "TestId", Name = "TestName", Price = 15, 
                ArticleGroup = new ArticleGroup() { Id = GuidCollection.Id005, Name = "Test", Articlegroup = null }}},
           new Position() {
            Amount = 7, Nr = 2, Id = GuidCollection.Id002,
            Article = new Article() { Id = GuidCollection.Id003, ArticleId = "TestId", Name = "TestName", Price = 15,
            ArticleGroup = new ArticleGroup() { Id = GuidCollection.Id005, Name = "Test", Articlegroup = null }}}
        };

        public PositionBllTest()
        {
            var profile = new PositionProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            _positionRepository = PositionRepositoryHelper.TestPositionRepository(_positions);
            _userManager = UserManagerTestHelper.TestUserManager<User>(_users);

            _userManager.Setup(x => x.IsInRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(
                (User user, string role) =>
                {
                    if (user.Id.Equals(GuidCollection.Id001) && role.Equals(Roles.Administrator.ToString()))
                        return true;
                    if (user.Id.Equals(GuidCollection.Id002) && role.Equals(Roles.User.ToString()))
                        return true;
                    return false;
                });

            _userManager.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync((string id) =>
            {
                return _users.First(x => x.Id.ToString().Equals(id));
            });

            _position = new PositionBll(_positionRepository.Object, mapper);
        }

        [Fact]
        public void Get_Position_As_Administrator_Not_Throw()
        {
            Func<Task> get = async () => { await _position.Get(GuidCollection.Id002); };
            get.Should().NotThrow<Exception>();
        }

        [Fact]
        public void Get_Position_As_User_Not_Throw()
        {
            Func<Task> get = async () => { await _position.Get(GuidCollection.Id001); };
            get.Should().NotThrow<Exception>();
        }

        [Fact]
        public void Delete_Position_As_User_Not_Throw()
        {
            var positionDto = new PositionDto()
            {
                Amount = 3,
                Nr = 1,
                Id = GuidCollection.Id001,
                Article = new Article()
                {
                    Id = GuidCollection.Id003,
                    ArticleId = "TestId",
                    Name = "TestName",
                    Price = 15,
                    ArticleGroup = new ArticleGroup() { Id = GuidCollection.Id005, Name = "Test", Articlegroup = null }
                }
            };
            Func<Task> delete = async () => { await _position.Delete(positionDto); };
            delete.Should().NotThrow<Exception>();
        }

        [Fact]
        public void Add_Position_As_User_Not_Throw_And_Not_Null()
        {
            var positionDto = new PositionDto()
            {
                Amount = 4,
                Nr = 3,
                Id = GuidCollection.Id006,
                Article = new Article()
                {
                    Id = GuidCollection.Id003,
                    ArticleId = "TestId",
                    Name = "TestName",
                    Price = 15,
                    ArticleGroup = new ArticleGroup() { Id = GuidCollection.Id005, Name = "Test", Articlegroup = null }
                }
            };
            Func<Task> add = async () => { await _position.Add(positionDto); };
            add.Should().NotThrow<Exception>();
            Func<Task> get = async () => { await _position.Get(GuidCollection.Id006); };
            get.Should().NotBeNull();
        }

        [Fact]
        public void Update_Position_As_User_Not_Throw_And_Not_Null()
        {
            var positionDto = new PositionDto() {
                Amount = 6,
                Nr = 3,
                Id = GuidCollection.Id006,
                Article = new Article()
                {
                    Id = GuidCollection.Id003,
                    ArticleId = "TestId",
                    Name = "TestName",
                    Price = 15,
                    ArticleGroup = new ArticleGroup() { Id = GuidCollection.Id005, Name = "Test", Articlegroup = null }
                }
            };
            Func<Task> update = async () => { await _position.Update(positionDto); };
            update.Should().NotThrow<Exception>();
            update.Should().NotBeNull();
        }
    }
}
