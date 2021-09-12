using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using zbw.Auftragsverwaltung.Core.Addresses.Contracts;
using zbw.Auftragsverwaltung.Core.Addresses.Entities;
using zbw.Auftragsverwaltung.Core.Addresses.Profiles;
using zbw.Auftragsverwaltung.Core.Customers.Contracts;
using zbw.Auftragsverwaltung.Core.Customers.Entities;
using zbw.Auftragsverwaltung.Core.Customers.Profiles;
using zbw.Auftragsverwaltung.Core.Test.Customers;
using zbw.Auftragsverwaltung.Core.Test.Helpers;
using zbw.Auftragsverwaltung.Core.Test.Users;
using zbw.Auftragsverwaltung.Core.Users.Contracts;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Core.Users.Profiles;
using zbw.Auftragsverwaltung.Core.Common.DataTransfer;
using System.Threading.Tasks;
using FluentAssertions;

namespace zbw.Auftragsverwaltung.Core.Test.DataTransfer
{
    public class DataTransferTest
    {
        private readonly Mock<IAddressRepository> _addressRepository; 
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ICustomerRepository> _customerRepository;
        private readonly Mock<UserManager<User>> _userManager;
        private static Customer cust1 = new Customer() { CustomerNr = "CU00001", Firstname = "Max", Lastname = "Muster", Website = string.Empty, UserId = GuidCollection.Id001, Id = GuidCollection.Id001 };
        private static Customer cust2 = new Customer() { CustomerNr = "CU00002", Firstname = "Lisa", Lastname = "Muster", Website = string.Empty, UserId = GuidCollection.Id002, Id = GuidCollection.Id002 };

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
            cust1,
            cust2
        };
        
        private readonly List<Address> _addresses = new List<Address>()
        {
            new Address(){Id = GuidCollection.Id006, CustomerId =  GuidCollection.Id001, Street = "Seestrasse", Number = 5, Location = string.Empty, Zip = "9000", Customer = cust1},
            new Address(){Id = GuidCollection.Id007, CustomerId =  GuidCollection.Id002, Street = "Im Grüntal", Number = 13, Location = string.Empty, Zip = "9300", Customer = cust2}
        };

        private readonly List<User> _usersList = new List<User>()
        {
            new User(){Id = GuidCollection.Id001, PasswordHash= "hiddenPassword"}, 
            new User(){Id = GuidCollection.Id002, PasswordHash= "secretPassword"}
        };

        public DataTransferTest()
        {
            var custProfile = new CustomerProfile();
            var userProfile = new UserProfile();
            var adrProfile = new AddressProfile();
            var custConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(custProfile));
            var userConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(userProfile));
            var adrConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(adrProfile));
            IMapper custMapper = new Mapper(custConfiguration);
            IMapper userMapper = new Mapper(userConfiguration);
            IMapper adrMapper = new Mapper(adrConfiguration);

            _customerRepository = CustomerRepositoryHelper.TestCustomerRepository(_customers);
            _addressRepository = AddressRepositoryHelper.TestAddressRepository(_addresses);
            _userRepository = UserRepositoryHelper.TestUserRepository(_usersList);
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
        }

        [Fact]
        public void Export_Xml_Customers_Not_Throw()
        {
            DataTransferXML dataTransfer = new DataTransferXML(_addressRepository.Object, _customerRepository.Object, _userRepository.Object);
            dataTransfer.export();
        }

        [Fact]
        public void Export_Json_Customers_Not_Throw()
        {
            DataTransferJSON dataTransfer = new DataTransferJSON(_addressRepository.Object, _customerRepository.Object, _userRepository.Object);
            dataTransfer.export();
        }


        [Fact]
        public void Import_Json_Customers_Not_Throw()
        {
            _customerRepository.Object.DeleteAsync(cust2);
            DataTransferJSON dataTransfer = new DataTransferJSON(_addressRepository.Object, _customerRepository.Object, _userRepository.Object);
            dataTransfer.import();
            Func<Task> get = async () => { await _customerRepository.Object.GetByIdAsync(GuidCollection.Id002); };
             get.Should().NotBeNull();
        }


    }
}
