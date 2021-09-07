using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace zbw.Auftragsverwaltung.BlazorApp.Components.Table
{
    public partial class TableSizeSelector : IDisposable
    {
        [Parameter]
        public int Value { get; set; }

        [Parameter]
        public EventCallback<ChangeEventArgs> OnChange { get; set; }

        [Parameter]
        public Dictionary<int, string> AvailableSelections { get; set; } = new Dictionary<int, string>()
        {
            {5, "5" },
            {10, "10" },
            {25, "25" },
            {50, "50" },
            {-1, "All" }
        };

        private void OnCountChange(ChangeEventArgs e)
        {
            if (!int.TryParse(e.Value.ToString(), out var val)) return;

            Value = val;
            NotifyOnChange(e);
        }

        private async void NotifyOnChange(ChangeEventArgs e) => await OnChange.InvokeAsync(e);

        public void Dispose()
        {
            OnChange = EventCallback<ChangeEventArgs>.Empty;
        }
    }
}
