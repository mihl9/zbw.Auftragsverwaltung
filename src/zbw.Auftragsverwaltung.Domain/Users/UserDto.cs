using System;
using System.Collections.Generic;
using zbw.Auftragsverwaltung.Domain.Customers;

namespace zbw.Auftragsverwaltung.Domain.Users
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
