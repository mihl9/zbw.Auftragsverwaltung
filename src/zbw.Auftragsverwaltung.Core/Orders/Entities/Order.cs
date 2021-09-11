using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;
using zbw.Auftragsverwaltung.Core.Customers.Entities;

namespace zbw.Auftragsverwaltung.Core.Orders.Entities
{
    public class Order : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public int OrderNr { get; set; }

        public virtual Customer Customer{ get; set; }

        public virtual Guid CustomerId { get; set; }

        public DateTime Date { get; set; }
    }
}
