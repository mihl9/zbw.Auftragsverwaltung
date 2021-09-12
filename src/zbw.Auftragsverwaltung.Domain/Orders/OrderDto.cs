using System;
using zbw.Auftragsverwaltung.Domain.Customers;

namespace zbw.Auftragsverwaltung.Domain.Orders
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public int OrderNr { get; set; }

        public CustomerDto Customer{ get; set; }

        public Guid CustomerId { get; set; }

        public DateTime Date { get; set; }
    }
}
