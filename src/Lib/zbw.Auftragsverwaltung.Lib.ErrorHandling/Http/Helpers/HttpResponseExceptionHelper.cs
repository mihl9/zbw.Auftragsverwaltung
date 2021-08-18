using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Helpers;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Http.Helpers
{
    public class HttpResponseExceptionHelper
    {

        public static bool RegisterExceptionCodeHeader(HttpContext context, string exceptionCode, bool overrideExisting = false)
        {
            if (context == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(exceptionCode))
            {
                return false;
            }

            if (!context.Response.Headers.ContainsKey(ErrorHandlerDefaults.ExceptionCode))
            {
                context.Response.Headers.Add(ErrorHandlerDefaults.ExceptionCode, exceptionCode);
            }
            else
            {
                var codes = context.Response.GetExceptionCode();

                if (overrideExisting || !codes.Any())
                {
                    context.Response.Headers[ErrorHandlerDefaults.ExceptionCode] = exceptionCode;
                }
                else
                {
                    if (!codes.Any(i => i.Equals(exceptionCode, StringComparison.OrdinalIgnoreCase)))
                    {
                        context.Response.Headers[ErrorHandlerDefaults.ExceptionCode] += ErrorHandlerDefaults.ExceptionCodeSeparator + exceptionCode;
                    }
                }
            }

            return true;
        }

    }
}
