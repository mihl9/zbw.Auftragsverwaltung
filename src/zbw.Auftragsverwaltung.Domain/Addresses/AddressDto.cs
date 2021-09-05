﻿using System;
using System.Collections.Generic;
using System.Text;

namespace zbw.Auftragsverwaltung.Domain.Addresses
{
    public class AddressDto
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public string Street { get; set; }

        public int Number { get; set; }

        public string Zip { get; set; }

        public string Location { get; set; }

        public string FullAddress { get; set; }
    }
}