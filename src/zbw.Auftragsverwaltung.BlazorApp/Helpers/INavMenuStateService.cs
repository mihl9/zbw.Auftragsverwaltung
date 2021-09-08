using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zbw.Auftragsverwaltung.BlazorApp.Helpers
{
    public interface INavMenuStateService
    {
        public event Action OnChanged;
        public void NotifyChanged();
    }
}
