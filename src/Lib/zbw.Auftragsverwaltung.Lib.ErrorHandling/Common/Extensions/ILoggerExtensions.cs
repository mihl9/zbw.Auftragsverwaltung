using System;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace zbw.Auftragsverwaltung.Lib.ErrorHandling.Common.Extensions
{
    public static class ILoggerExtensions
    {
        public static void LogException(
            this ILogger logger,
            LogLevel level,
            string message,
            string messageId,
            string requestId = null,
            string method = null,
            string url = null,
            Exception ex = null)
        {
            var msgBuilder = new StringBuilder();
            msgBuilder.Append($"{message} ");
            msgBuilder.Append($"MESSAGEID: {messageId} ");

            if (!string.IsNullOrWhiteSpace(requestId))
            {
                msgBuilder.Append($"// REQUESTID: {requestId} ");
            }

            if (!string.IsNullOrWhiteSpace(method))
            {
                msgBuilder.Append($"// METHOD: {method} ");
            }

            if (!string.IsNullOrWhiteSpace(url))
            {
                msgBuilder.Append($"// URL: {url} ");
            }

            if (!string.IsNullOrWhiteSpace(ex?.Message))
            {
                msgBuilder.Append($"// EXCEPTION: {ex.Message} ");
            }

            var msg = msgBuilder.ToString().Trim();
            logger.Log(level, msg);
        }

        public static void LogException(
            this ILogger logger,
            LogLevel level,
            string message,
            string messageId,
            HttpContext context,
            Exception ex = null)
        {
            LogException(logger, level, message, messageId, context?.TraceIdentifier, context?.Request?.Method, context?.Request?.Path, ex);
        }

    }
}
