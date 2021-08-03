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
using zbw.Auftragsverwaltung.Core.Customers.BLL;
using zbw.Auftragsverwaltung.Core.Customers.Contracts;
using zbw.Auftragsverwaltung.Core.Customers.Entities;
using zbw.Auftragsverwaltung.Core.Customers.Profiles;
using zbw.Auftragsverwaltung.Core.Test.Helpers;
using zbw.Auftragsverwaltung.Core.Test.Users;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;

namespace zbw.Auftragsverwaltung.Core.Test.Customers
{
    public class CustomerBllTest
    {
        private readonly CustomerBll _customer;
        private readonly Mock<ICustomerRepository> _customerRepository;
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

        private readonly List<Customer> _customers = new List<Customer>()
        {
            new Customer(){CustomerNr = 1, Firstname = "Michael", Lastname = "Huber", Website = string.Empty, UserId = GuidCollection.Id001, Id = GuidCollection.Id001},
            new Customer(){CustomerNr = 2, Firstname = "Test", Lastname = "Test", Website = string.Empty, UserId = GuidCollection.Id002, Id = GuidCollection.Id002}
        };

        public CustomerBllTest()
        {
            var profile = new CustomerProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            IMapper mapper = new Mapper(configuration);

            _customerRepository = CustomerRepositoryHelper.TestCustomerRepository(_customers);
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

            _customer = new CustomerBll(_customerRepository.Object, mapper, _userManager.Object);
        }

        [Fact]
        public void Get_Not_Assigned_Customer_As_Administrator_Not_Throw()
        {
            Func<Task> get = async () => { await _customer.Get(GuidCollection.Id002, GuidCollection.Id001); };
            get.Should().NotThrow<Exception>();
        }

        [Fact]
        public void Get_Not_Assigned_Customer_As_User_Throw()
        {
            Func<Task> get = async () => { await _customer.Get(GuidCollection.Id001, GuidCollection.Id002); };
            get.Should().Throw<InvalidRightsException>();
        }

        [Fact]
        public void Get_Assigned_Customer_As_User_Not_Throw()
        {
            Func<Task> get = async () => { await _customer.Get(GuidCollection.Id002, GuidCollection.Id002); };
            get.Should().NotThrow<Exception>();
        }
    }
}
