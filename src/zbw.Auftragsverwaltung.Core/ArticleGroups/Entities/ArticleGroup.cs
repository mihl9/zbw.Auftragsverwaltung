using System;
using System.ComponentModel.DataAnnotations;
using zbw.Auftragsverwaltung.Core.Common.Interfaces;

namespace zbw.Auftragsverwaltung.Core.ArticleGroups.Entities
{
    public class ArticleGroup : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ArticleGroup Articlegroup { get; set; }

        public Guid UserId { get; set; }
    }
}
