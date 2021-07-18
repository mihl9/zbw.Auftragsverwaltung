using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;

namespace zbw.Auftragsverwaltung.Core.Orders.Entities
{
    public class Order : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public int OrderNr { get; set; }

        public int CustomerNr { get; set; }

        public DateTime Date { get; set; }
    }
}
