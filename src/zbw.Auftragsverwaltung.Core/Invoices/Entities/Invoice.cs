using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using zbw.Auftragsverwaltung.Core.Addresses.Entities;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;

namespace zbw.Auftragsverwaltung.Core.Invoices.Entities
{
    public class Invoice : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public virtual Address Address { get; set; }

        public virtual Guid AddressId { get; set; }

        public DateTime Date { get; set; }

        public int Number { get; set; }

        public double Netto { get; set; }

        public double Brutto { get; set; }

        public double Tax { get; set; }

        public virtual DateTime AdressValidFrom { get; set; }

    }
}
