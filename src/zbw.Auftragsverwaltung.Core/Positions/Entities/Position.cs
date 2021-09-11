using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;
using zbw.Auftragsverwaltung.Core.Articles.Entities;

namespace zbw.Auftragsverwaltung.Core.Positions.Entities
{
    public class Position : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public int Nr { get; set; }

        public virtual Article Article { get; set; }

        public virtual Guid ArticleId { get; set; }

        public int Amount { get; set; }
    }
}
