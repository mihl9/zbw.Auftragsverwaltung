using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Models;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Extensions
{
    public static class ProblemDetailsExtensions
    {
        public static ProblemDetails AddExtension(this ProblemDetails details, string key, object content,
            bool overwrite)
        {
            if (!details.Extensions.ContainsKey(key))
            {
                details.Extensions.Add(key, content);
            }else if (overwrite)
            {
                details.Extensions[key] = content;
            }

            return details;
        }
    }
}
