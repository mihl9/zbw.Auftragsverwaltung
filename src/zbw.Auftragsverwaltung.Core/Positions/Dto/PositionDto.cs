using System;
using System.Collections.Generic;
using System.Text;


namespace zbw.Auftragsverwaltung.Core.Positions.Dto
{
    public class PositionDto
    {
        public Guid Id { get; set; }

        public int Nr { get; set; }

        // public Article article { get; set; }
        public int Amount { get; set; }
    }
}
