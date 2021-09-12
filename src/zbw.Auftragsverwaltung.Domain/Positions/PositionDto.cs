using System;
using zbw.Auftragsverwaltung.Domain.Articles;

namespace zbw.Auftragsverwaltung.Domain.Positions
{
    public class PositionDto
    {
        public Guid Id { get; set; }

        public int Nr { get; set; }

        public ArticleDto Article { get; set; }

        public Guid ArticleId { get; set; }

        public int Amount { get; set; }
    }
}
