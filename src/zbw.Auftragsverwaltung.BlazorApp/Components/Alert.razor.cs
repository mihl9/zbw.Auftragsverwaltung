using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using zbw.Auftragsverwaltung.BlazorApp.Helpers;

namespace zbw.Auftragsverwaltung.BlazorApp.Components
{
    public partial class Alert
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public HighlightLevel Level { get; set; }

        [Parameter]
        public bool Dismissible { get; set; }

        [Parameter]
        public bool Visible { get; set; }

        public void OnDismissClick()
        {
            Visible = false;
            StateHasChanged();
        }

        public void Show(string message, HighlightLevel alertLevel)
        {
            Level = alertLevel;
            ChildContent = builder => builder.AddContent(0, message);
            Visible = true;
            StateHasChanged();
        }

        public void Close()
        {
            OnDismissClick();
        }

	}
}
