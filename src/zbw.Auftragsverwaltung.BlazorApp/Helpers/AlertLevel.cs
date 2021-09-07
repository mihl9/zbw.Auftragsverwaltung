using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zbw.Auftragsverwaltung.BlazorApp.Helpers
{
    public class HighlightLevel
    {
        public static HighlightLevel Primary = new HighlightLevel("primary");
        public static HighlightLevel Info = new HighlightLevel("info");
        public static HighlightLevel Success = new HighlightLevel("success");
        public static HighlightLevel Danger = new HighlightLevel("danger");
        public static HighlightLevel Warning = new HighlightLevel("warning");
        public static HighlightLevel Default = new HighlightLevel("default");

        public string Level { get; set; }

        public HighlightLevel(string level)
        {
            Level = level;
        }

	}
}
