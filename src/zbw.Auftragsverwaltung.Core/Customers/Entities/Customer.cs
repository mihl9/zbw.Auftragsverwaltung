using System;
using System.ComponentModel.DataAnnotations;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;

namespace zbw.Auftragsverwaltung.Core.Customers.Entities
{
    public class Customer : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public int CustomerNr { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Website { get; set; }
    }
}
