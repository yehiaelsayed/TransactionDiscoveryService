using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace TransactonDiscovery.Utils
{
    public class Logger
    {

        private static ILogger _logger;

        public static string CorrelationID;
        public static void CreateLogger(LogLevel level)
        {
            try
            {
                var loggerFatory = LoggerFactory.Create(config =>
                 {
                     config.AddConsole();
                     config.SetMinimumLevel(level);
                 });

                _logger = loggerFatory.CreateLogger("GlobalLogger");
            }
            catch { }

        }

        public static void Info(string message, params object[] args)
        {
            _logger?.LogInformation(FormatMessage(message) , args, CorrelationID);
        }

        public static void Error(Exception exception, string message = "", params object[] args)
        {
            _logger?.LogError(exception, FormatMessage(message), args, CorrelationID);
        }

        public static void Debug(string message, Exception exception, params object[] args)
        {
            _logger?.LogDebug(FormatMessage(message), args, CorrelationID);
        }

        private static string FormatMessage(string message)
        {

            return $"{message} - {CorrelationID}";
        }

    }
}
