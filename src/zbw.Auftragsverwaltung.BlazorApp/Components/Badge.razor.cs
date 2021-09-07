using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using zbw.Auftragsverwaltung.BlazorApp.Helpers;

namespace zbw.Auftragsverwaltung.BlazorApp.Components
{
    public partial class Badge
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public HighlightLevel Level { get; set; }

    }
}
