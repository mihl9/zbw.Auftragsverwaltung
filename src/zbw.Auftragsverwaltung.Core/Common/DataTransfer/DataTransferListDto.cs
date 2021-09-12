using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using zbw.Auftragsverwaltung.Domain.Addresses;
using zbw.Auftragsverwaltung.Core.Customers.Entities;
using zbw.Auftragsverwaltung.Core.Users.Entities;

namespace zbw.Auftragsverwaltung.Core.Common.DataTransfer
{
    [XmlRoot(ElementName = "Kunden", Namespace = "")]
    public class DataTransferListDto
    {
        [XmlElement(ElementName = "Kunde", Namespace = "")]
        public List<DataTransferDto> list { get; set; }

        public DataTransferListDto() {
            list = new List<DataTransferDto>();
        }
    }

    public class DataTransferDto
    {
        public string customerNr { get; set; }

        public string name { get; set; }

        public AddressDto address { get; set; }

        public Guid userId { get; set; }

        public string email { get; set; }
        public string website { get; set; }

        public string password { get; set; }


        [JsonConstructorAttribute]

        public DataTransferDto(string customerNr, string name, AddressDto address, Guid userId, string email, string website, string password)
        {
            this.customerNr = customerNr;
            this.name = name;
            this.address = address;
            this.userId = userId;
            this.email = email;
            this.website = website;
            this.password = password;
        } 
        public DataTransferDto(Customer customer, User user, AddressDto address)
        {
            this.customerNr = "CU" + customer.CustomerNr;
            this.name = customer.Firstname + " " + customer.Lastname;
            this.address = address;
            this.userId = customer.UserId;
            this.email = user.Email;
            this.website = customer.Website;
            this.password = user.PasswordHash;
        }
        public DataTransferDto()
        {
        }
    }
}
