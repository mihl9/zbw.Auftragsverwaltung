using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.Addresses.Entities;
using zbw.Auftragsverwaltung.Core.Customers.Entities;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using zbw.Auftragsverwaltung.Domain.Addresses;
using zbw.Auftragsverwaltung.Core.Addresses.Contracts;
using zbw.Auftragsverwaltung.Core.Customers.Contracts;
using zbw.Auftragsverwaltung.Core.Users.Contracts;
using System.IO;

namespace zbw.Auftragsverwaltung.Core.Common.DataTransfer
{
    public abstract class DataTransferAbstract
    {
        protected IAddressRepository addressRepository;
        protected ICustomerRepository customerRepository;
        protected IUserRepository userRepository;
        protected static string FILE_PATH = "C:\\";

        public DataTransferAbstract(IAddressRepository addressRepository, ICustomerRepository customerRepository, IUserRepository userRepository)
        {
            this.addressRepository = addressRepository;
            this.customerRepository = customerRepository;
            this.userRepository = userRepository;
        }

        public abstract void import();

        public abstract void export();

        public DataTransferDto getTransferObject(Address address)
        {
            Customer customer = customerRepository.GetByIdAsync(address.CustomerId).Result;
            User user = userRepository.GetByIdAsync(customer.UserId).Result;
            user.PasswordHash = encrypt(user.PasswordHash);
            DataTransferDto data = new DataTransferDto(customer, user, getAddressDto(address));
            return data;
        }

        public DataTransferListDto createTransferObjectList()
        {
            DataTransferListDto transferObjects = new DataTransferListDto();
            foreach (Address address in getAddressesAsync().Result)
            {
                transferObjects.list.Add(getTransferObject(address));
            }
            return transferObjects;
        }

        private AddressDto getAddressDto(Address address)
        {
            var fullAddress = $"{address.Street} {address.Number}, {address.Zip} {address.Location}";
            AddressDto addressDto = new AddressDto();
            addressDto.CustomerId = address.CustomerId;
            addressDto.FullAddress = fullAddress;
            addressDto.Id = address.Id;
            addressDto.Location = address.Location;
            addressDto.Number = address.Number;
            addressDto.Street = address.Street;
            addressDto.Zip = address.Zip;
            return addressDto;
        }

        protected async void createCustomers(DataTransferListDto list)
        {
            foreach (DataTransferDto data in list.list)
            {
                Customer customer = new Customer();
                Address address = new Address();
                customer.CustomerNr = Convert.ToInt32(data.customerNr.Remove(0, 2));
                customer.Firstname = data.name.Split(" ")[0];
                customer.Lastname = data.name.Split(" ")[1];
                customer.UserId = data.userId;
                customer.Website = data.website;
                address.Id = data.address.Id;
                address.Street = data.address.Street;
                address.Customer = customer;
                address.CustomerId = customer.Id;
                address.Location = data.address.Location;
                address.Number = data.address.Number;
                address.Zip = data.address.Zip;
                await customerRepository.AddAsync(customer);
                await addressRepository.AddAsync(address);

            }
        }


        public string encrypt(string password)
        {
            try
            {
                byte[] encByte = new byte[password.Length];
                encByte = Encoding.UTF8.GetBytes(password);
                return Convert.ToBase64String(encByte);
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public string decrypt(string encodedPassword)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            Decoder decoder = encoder.GetDecoder();
            byte[] toDecode = Convert.FromBase64String(encodedPassword);
            char[] decoded = new char[decoder.GetCharCount(toDecode, 0, toDecode.Length)];
            decoder.GetChars(toDecode, 0, toDecode.Length, decoded, 0);
            return new string(decoded);
        }

        protected async System.Threading.Tasks.Task<IReadOnlyList<Address>> getAddressesAsync()
        {
            return await addressRepository.ListAsync();
        }

    }
}
