using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace zbw.Auftragsverwaltung.BlazorApp.Components
{
    public partial class Card
    {
        [Parameter]
        public string MaterialIcon { get; set; }

        [Parameter]
        public RenderFragment Title { get; set; }

        [Parameter]
        public RenderFragment Body { get; set; }

        [Parameter]
        public RenderFragment Footer { get; set; }

        public string GetCardClasses()
        {
            var c = string.Empty;

            if (!string.IsNullOrEmpty(MaterialIcon))
            {
                c += "card-header-icon";
            }
            else
            {
                c += "card-header-text";
            }

            return c;
        }

	}
}
