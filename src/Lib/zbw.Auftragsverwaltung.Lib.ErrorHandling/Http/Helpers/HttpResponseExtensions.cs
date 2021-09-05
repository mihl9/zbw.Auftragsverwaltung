using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Helpers;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Helpers
{
    public static class HttpResponseExtensions
    {

        public static string[] GetExceptionCode(this HttpResponse response)
        {
            var codes = Array.Empty<string>();

            if (response?.Headers != null && response.Headers.ContainsKey(ErrorHandlerDefaults.ExceptionCode))
            {
                var rawCodes = response.Headers[ErrorHandlerDefaults.ExceptionCode].ToString();
                codes = rawCodes
                    .Split(ErrorHandlerDefaults.ExceptionCodeSeparator)
                    .Select(c => c.Trim())
                    .Where(c => !string.IsNullOrWhiteSpace(c))
                    .ToArray();
            }

            return codes;
        }

    }
}
