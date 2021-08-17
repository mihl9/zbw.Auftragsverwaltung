using System;

namespace zbw.Auftragsverwaltung.Domain.Customers
{
    public class CustomerDto
    {
        public Guid Id { get; set; }

        public int CustomerNr { get; set; }

        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Website { get; set; }

        public string UserId { get; set; }
    }
}
