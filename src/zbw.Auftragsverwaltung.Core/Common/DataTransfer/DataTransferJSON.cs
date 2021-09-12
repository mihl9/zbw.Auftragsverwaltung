
using System;
using System.IO;
using System.Text.Json;
using zbw.Auftragsverwaltung.Core.Addresses.Contracts;
using zbw.Auftragsverwaltung.Core.Customers.Contracts;
using zbw.Auftragsverwaltung.Core.Users.Contracts;

namespace zbw.Auftragsverwaltung.Core.Common.DataTransfer
{
    public class DataTransferJSON : DataTransferAbstract
    {
        private string FILENAME = "CustomerData.json";

        public DataTransferJSON(IAddressRepository addressRepository, ICustomerRepository customerRepository, IUserRepository userRepository) : base(addressRepository, customerRepository, userRepository)
        {
        }
        public async override void export()
        {
            var exportList = createTransferObjectList();
            var inputStream = File.Create(FILE_PATH + FILENAME); 
            var options = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };
            await JsonSerializer.SerializeAsync(inputStream, exportList, options);
            inputStream.Close();
        }


        public async override void import()
        {
            if (File.Exists(FILE_PATH + FILENAME))
            {
                var inputStream = File.Open(FILE_PATH + FILENAME, FileMode.Open);
                var list = (DataTransferListDto) await JsonSerializer.DeserializeAsync(inputStream, typeof(DataTransferListDto));
                createCustomers(list);
                inputStream.Close();
            }
        }

    }
}
