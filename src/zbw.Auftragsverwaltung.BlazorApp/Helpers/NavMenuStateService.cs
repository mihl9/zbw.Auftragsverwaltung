using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace zbw.Auftragsverwaltung.BlazorApp.Helpers
{
    public class NavMenuStateService : INavMenuStateService
    {
        public event Action OnChanged;
        public void NotifyChanged()
        {
            OnChanged?.Invoke();
        }
    }
}
