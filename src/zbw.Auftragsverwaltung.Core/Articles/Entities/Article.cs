using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using zbw.Auftragsverwaltung.Core.ArticleGroups.Entities;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;

namespace zbw.Auftragsverwaltung.Core.Articles.Entities
{
    public class Article : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string ArticleId { get; set; }

        public string Name { get; set; }

        public int Price { get; set; }

        public virtual Guid ArticleGroupId { get; set; }

        public virtual ArticleGroup ArticleGroup { get; set; }

    }
}
