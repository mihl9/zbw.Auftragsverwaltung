using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace zbw.Auftragsverwaltung.BlazorApp.Components
{
    public partial class RefreshButton : IDisposable
    {
        private const string SpinningClass = "fa-spin";

        private async Task HandleRefreshClick()
        {
            await NotifyRefresh();
        }

        private void OnMouseIn(MouseEventArgs e)
        {
            Animation = SpinningClass;
        }

        private void OnMouseOut(MouseEventArgs e)
        {
            Animation = string.Empty;
        }

        [Parameter]
        public EventCallback OnRefresh { get; set; }

        private string Animation { get; set; }

        private async Task NotifyRefresh() => await OnRefresh.InvokeAsync(this);

        public void Dispose()
        {
            OnRefresh = EventCallback.Empty;
        }

	}
}
