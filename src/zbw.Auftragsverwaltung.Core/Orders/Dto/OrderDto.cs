using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.Customers.Entities;


namespace zbw.Auftragsverwaltung.Core.Orders.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }

        public int OrderNr { get; set; }

        public Customer Customer { get; set; }

        public DateTime Date { get; set; }
    }
}
