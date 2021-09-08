using System;
using System.Collections.Generic;
using System.Text;
using zbw.Auftragsverwaltung.Core.Articles.Dto;


namespace zbw.Auftragsverwaltung.Core.Positions.Dto
{
    public class PositionDto
    {
        public Guid Id { get; set; }

        public int Nr { get; set; }

        public ArticleDto Article { get; set; }

        public int Amount { get; set; }
    }
}
