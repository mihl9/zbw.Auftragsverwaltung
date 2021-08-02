using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.Customers.Dto;

namespace zbw.Auftragsverwaltung.Core.Users.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<CustomerDto> AssignedCustomers { get; set; }
    }
}
