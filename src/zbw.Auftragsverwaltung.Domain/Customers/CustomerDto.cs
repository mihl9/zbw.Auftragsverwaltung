using System;
using System.ComponentModel.DataAnnotations;

namespace zbw.Auftragsverwaltung.Domain.Customers
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        [RegularExpression(@"CU\d{5}")]
        public int CustomerNr { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        [Url]
        public string Website { get; set; }

        public string UserId { get; set; }
    }
}
