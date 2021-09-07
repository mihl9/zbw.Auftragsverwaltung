using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace zbw.Auftragsverwaltung.BlazorApp.Components
{
    public partial class DynamicContentLoader
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool HideContent { get; set; }

    }
}
