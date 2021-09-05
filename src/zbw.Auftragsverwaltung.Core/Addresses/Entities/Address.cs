using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;
using zbw.Auftragsverwaltung.Core.Customers.Entities;

namespace zbw.Auftragsverwaltung.Core.Addresses.Entities
{
    public class Address : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public virtual Guid CustomerId { get; set; }
        
        public virtual Customer Customer { get; set; }

        public string Street { get; set; }

        public int Number { get; set; }
        
        public string Zip { get; set; }

        public string Location { get; set; }

    }
}
