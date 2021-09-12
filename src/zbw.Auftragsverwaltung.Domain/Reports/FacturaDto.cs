using System;
using System.Collections.Generic;
using System.Text;

namespace zbw.Auftragsverwaltung.Domain.Reports
{
    public class FacturaDto
    {
        public string CustomerNr {get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public string Zip { get; set; }

        public string Location { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string InvoiceNumber { get; set; }

        public double Brutto { get; set; }

        public double Netto { get; set; }
    }
}
