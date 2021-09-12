using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using zbw.Auftragsverwaltung.Domain.Customers;

using zbw.Auftragsverwaltung.Domain.Addresses;
using zbw.Auftragsverwaltung.Domain.Users;
using Newtonsoft.Json.Linq;
using System.Xml;
using zbw.Auftragsverwaltung.Core.Customers.Contracts;
using AutoMapper;
using zbw.Auftragsverwaltung.Core.Users.Entities;
using Microsoft.AspNetCore.Identity;
using zbw.Auftragsverwaltung.Core.Addresses.Contracts;
using zbw.Auftragsverwaltung.Core.Users.Contracts;
using zbw.Auftragsverwaltung.Core.Addresses.BLL;
using zbw.Auftragsverwaltung.Core.Customers.BLL;
using zbw.Auftragsverwaltung.Core.Users.Bll;
using zbw.Auftragsverwaltung.Core.Customers.Entities;
using System.Xml.Serialization;

namespace zbw.Auftragsverwaltung.Core.Common.DataTransfer
{
    public class DataTransferXML : DataTransferAbstract 
    {
        private string FILENAME = "CustomerData.xml";
        public DataTransferXML(IAddressRepository addressRepository, ICustomerRepository customerRepository, IUserRepository userRepository) : base(addressRepository, customerRepository, userRepository) { 


        }

        public override void import()
        {
            if(File.Exists(FILE_PATH + FILENAME))
            {
                XmlSerializer ser = new XmlSerializer(typeof(DataTransferListDto));
                
                using (XmlReader reader = XmlReader.Create(FILE_PATH + FILENAME))
                {
                    DataTransferListDto list  = (DataTransferListDto)ser.Deserialize(reader);
                    createCustomers(list);
                }
                
            }
        }

        public override void export()
        {
            var exportList =  createTransferObjectList();
            var inputStream = File.Create(FILE_PATH + FILENAME);
            var serializer = new XmlSerializer(typeof(DataTransferListDto));
            var writer = new StringWriter(); 
            var settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };
            var ns = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
    
     using (var stream = new StringWriter())
     using (var writer2 = XmlWriter.Create(inputStream, settings))
     {
                serializer.Serialize(writer, exportList, ns);
                var serializedXml = writer.ToString();
                byte[] data = new UTF8Encoding(true).GetBytes(serializedXml);
                inputStream.Write(data);
            }
            
        }
    }
}
