using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;

namespace zbw.Auftragsverwaltung.Core.Positions.Entities
{
    public class Position : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public int Nr { get; set; }

        // public Article article { get; set; }
        public int Amount { get; set; }
    }
}
