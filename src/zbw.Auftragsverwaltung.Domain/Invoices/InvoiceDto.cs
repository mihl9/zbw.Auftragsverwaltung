using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Domain.Addresses;

namespace zbw.Auftragsverwaltung.Domain.Invoices
{
    public class InvoiceDto
    {
        public Guid Id { get; set; }

        public AddressDto Address { get; set; }

        public DateTime Date { get; set; }

        public int Number { get; set; }

        public double Netto { get; set; }

        public double Brutto { get; set; }

        public double Tax { get; set; }

    }
}
