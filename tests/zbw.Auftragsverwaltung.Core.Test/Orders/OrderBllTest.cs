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
using zbw.Auftragsverwaltung.Core.Orders.BLL;
using zbw.Auftragsverwaltung.Core.Orders.Contracts;
using zbw.Auftragsverwaltung.Core.Orders.Entities;
using zbw.Auftragsverwaltung.Core.Orders.Dto;
using zbw.Auftragsverwaltung.Core.Orders.Profiles;
using zbw.Auftragsverwaltung.Core.Test.Helpers;
using zbw.Auftragsverwaltung.Core.Test.Users;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Core.Customers.Entities;

namespace zbw.Auftragsverwaltung.Core.Test.Orders
{
    public class OrderBllTest
    {
        private readonly OrderBll _order;
        private readonly Mock<IOrderRepository> _orderRepository;
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

        private readonly List<Order> _orders = new List<Order>()
        {
            new Order(){Customer = new Customer(){CustomerNr = 1, Firstname = "Max", Lastname = "Muster", Website = string.Empty, UserId = GuidCollection.Id001, Id = GuidCollection.Id001}, OrderNr = 3, Date = new DateTime(2008,11,20),Id = GuidCollection.Id001},
            new Order(){Customer = new Customer(){CustomerNr = 2, Firstname = "Lisa", Lastname = "Muster", Website = string.Empty, UserId = GuidCollection.Id002, Id = GuidCollection.Id002}, OrderNr = 2, Date = new DateTime(2017,03,15), Id = GuidCollection.Id002}
        };

        public OrderBllTest()
        {
            var profile = new OrderProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            _orderRepository = OrderRepositoryHelper.TestOrderRepository(_orders);
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

            _order = new OrderBll(_orderRepository.Object, mapper);
        }

        [Fact]
        public void Get_Order_As_Administrator_Not_Throw()
        {
            Func<Task> get = async () => { await _order.Get(GuidCollection.Id002); };
            get.Should().NotThrow<Exception>();
        }

        [Fact]
        public void Get_Order_As_User_Not_Throw()
        {
            Func<Task> get = async () => { await _order.Get(GuidCollection.Id001); };
            get.Should().NotThrow<Exception>();
        }

        [Fact]
        public void Delete_Order_As_User_Not_Throw()
        {
            var orderDto = new OrderDto() { Customer = new Customer() { CustomerNr = 1, Firstname = "Max", Lastname = "Muster", Website = string.Empty, UserId = GuidCollection.Id001, Id = GuidCollection.Id001 }, OrderNr = 3, Date = new DateTime(2008,11,20), Id = GuidCollection.Id001 };
            Func<Task> delete = async () => { await _order.Delete(orderDto); };
            delete.Should().NotThrow<Exception>();
         }

        [Fact]
        public void Add_Order_As_User_Not_Throw_And_Not_Null()
        {
            var orderDto = new OrderDto() { Customer = new Customer() { CustomerNr = 3, Firstname = "Minnie", Lastname = "Muster", Website = string.Empty, UserId = GuidCollection.Id003, Id = GuidCollection.Id003 }, OrderNr = 3, Date = new DateTime(2017,03,15), Id = GuidCollection.Id003 };
            Func<Task> add = async () => { await _order.Add(orderDto); };
            add.Should().NotThrow<Exception>();
            Func<Task> get = async () => { await _order.Get(GuidCollection.Id003); };
            get.Should().NotBeNull();
        }

        [Fact]
        public void Update_Order_As_User_Not_Throw_And_Not_Null()
        {
            var orderDto = new OrderDto(){ Customer = new Customer() { CustomerNr = 2, Firstname = "Lisa", Lastname = "Muster", Website = string.Empty, UserId = GuidCollection.Id002, Id = GuidCollection.Id002 }, OrderNr = 2, Date = new DateTime(2017,04,15), Id = GuidCollection.Id002};
            Func<Task> update = async () => { await _order.Update(orderDto); };
            update.Should().NotThrow<Exception>();
            update.Should().NotBeNull();
        }
    }
}