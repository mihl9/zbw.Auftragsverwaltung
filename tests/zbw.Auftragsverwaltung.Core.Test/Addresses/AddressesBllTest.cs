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
using zbw.Auftragsverwaltung.Core.Addresses.BLL;
using zbw.Auftragsverwaltung.Core.Addresses.Contracts;
using zbw.Auftragsverwaltung.Core.Addresses.Entities;
using zbw.Auftragsverwaltung.Core.Addresses.Profiles;
using zbw.Auftragsverwaltung.Core.ArticleGroups.BLL;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Contracts;
using zbw.Auftragsverwaltung.Core.Common.Exceptions;
using zbw.Auftragsverwaltung.Core.Customers.BLL;
using zbw.Auftragsverwaltung.Core.Customers.Contracts;
using zbw.Auftragsverwaltung.Core.Customers.Entities;
using zbw.Auftragsverwaltung.Core.Customers.Profiles;
using zbw.Auftragsverwaltung.Core.Test.Customers;
using zbw.Auftragsverwaltung.Core.Test.Helpers;
using zbw.Auftragsverwaltung.Core.Test.Users;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Core.Users.Enumerations;
using zbw.Auftragsverwaltung.Domain.Addresses;

namespace zbw.Auftragsverwaltung.Core.Test.Addresses
{
    public class InvoiceBllTest
    {
        private readonly AddressBll _address;
        private readonly CustomerBll _customer;
        private readonly Mock<IAddressRepository> _addressRepository;
        private readonly Mock<ICustomerRepository> _customerRepository;
        private readonly Mock<UserManager<User>> _userManager;


        private readonly List<Address> _addresses = new List<Address>()
        {
            new Address
            {
                Id = GuidCollection.Id001,
                CustomerId = GuidCollection.Id002,
                Street = "Musterstrasse",
                Number = 5,
                Zip = "9000",
                Recipient = "Musterstrasse 5, 9000 St.Gallen",
                Location = "St. Gallen",
                ValidFrom = new DateTime(2020,5,5),
                ValidTo = null
            },
            new Address
            {
                Id = GuidCollection.Id003,
                CustomerId = GuidCollection.Id002,
                Street = "Musterstrasse",
                Number = 5,
                Zip = "9000",
                Recipient = "Musterstrasse 5, 9000 St.Gallen",
                Location = "St. Gallen",
                ValidFrom = new DateTime(2020,5,5),
                ValidTo = null
            }
        };

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
            new Customer(){CustomerNr = "CU0001", Firstname = "Michael", Lastname = "Huber", Website = string.Empty, UserId = GuidCollection.Id001, Id = GuidCollection.Id001},
            new Customer(){CustomerNr = "CU0002", Firstname = "Test", Lastname = "Test", Website = string.Empty, UserId = GuidCollection.Id002, Id = GuidCollection.Id002}
        };

        public InvoiceBllTest()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AddressProfile());
                cfg.AddProfile(new CustomerProfile());
            });
            IMapper mapper = new Mapper(configuration);

            _addressRepository = AdressRepositoryHelper.TestAdressRepository(_addresses);
            _userManager = UserManagerTestHelper.TestUserManager<User>(_users);
            _customerRepository = CustomerRepositoryHelper.TestCustomerRepository(_customers);

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
            _address = new AddressBll(_addressRepository.Object,mapper,_userManager.Object,_customer);
        }

        [Fact]
        public void Get_Address_As_Administrator_Not_Throw()
        {
            Func<Task> get = async () => { await _address.Get(GuidCollection.Id001, GuidCollection.Id001); };
            get.Should().NotThrow<Exception>();
        }
        [Fact]
        public void Delete_Address_Not_Throw()
        {
            var addressDto = new AddressDto
            {
                Id = GuidCollection.Id004,
                CustomerId = GuidCollection.Id002,
                Street = "Musterstrasse",
                Number = 5,
                Zip = "9000",
                Recipient = "Musterstrasse 5, 9000 St.Gallen",
                Location = "St. Gallen",
                ValidFrom = new DateTime(2020,5,5),
                ValidTo = null
            };
            Func<Task> delete = async () => { await _address.Delete(addressDto, GuidCollection.Id001); };
            delete.Should().NotThrow<Exception>();
        }

        [Fact]
        public void Add_Address_Not_Null()
        {
            var addressDto = new AddressDto
            {
                Id = GuidCollection.Id004,
                CustomerId = GuidCollection.Id002,
                Street = "Musterstrasse",
                Number = 5,
                Zip = "9000",
                Recipient = "Musterstrasse 5, 9000 St.Gallen",
                Location = "St. Gallen",
                ValidFrom = new DateTime(2020,5,5),
                ValidTo = null
            };
            Func<Task> add = async () => { await _address.Add(addressDto, GuidCollection.Id001); };
            add.Should().NotThrow<Exception>();
            Func<Task> get = async () => { await _address.Get(GuidCollection.Id004,GuidCollection.Id001); };
            get.Should().NotBeNull();
        }

        [Fact]
        public void Update_Address_Not_Null()
        {
            var addressDto = new AddressDto
            {
                Id = GuidCollection.Id001,
                CustomerId = GuidCollection.Id002,
                Street = "Musterstrasse",
                Number = 5,
                Zip = "9000",
                Recipient = "Beispielstrasse 5, 9000 St.Gallen",
                Location = "St. Gallen",
                ValidFrom = new DateTime(2020,5,5),
                ValidTo = null
            };
            Func<Task> update = async () => { await _address.Update(addressDto,GuidCollection.Id002); };
            update.Should().NotThrow<Exception>();
            update.Should().NotBeNull();
        }

    }
}
