using System;
using System.Collections.Generic;
using System.Text;

namespace zbw.Auftragsverwaltung.Core.Orders.Dto
{
    class OrderDto
    {
        public Guid Id { get; set; }

        public int OrderNr { get; set; }

        public int CustomerNr { get; set; }

        public DateTime Date { get; set; }
    }
}
