using LightResults;
using Microsoft.Extensions.Logging;

namespace WROBoxLabelGeneration.SharedKernel.Logger
{
    public static class LoggerHelper
    {
        private static ILoggerFactory _loggerFactory;
        private static ILogger _logger;
        private static string _serviceID = "WroBoxLabelGeneration ***";

        public static void InitializeLogger(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = _loggerFactory.CreateLogger("GlobalLogger");
        }

        public static ILogger GetLogger()
        {
            if (_logger == null)
            {
                throw new InvalidOperationException("Logger is not initialized.");
            }

            return _logger;
        }

        public static void LogInformation(string message)
        {
            GetLogger().LogInformation(FormatMessage(message));
        }

        public static void LogWarning(string message)
        {
            GetLogger().LogWarning(FormatMessage(message));
        }

        public static void LogError(string message)
        {
            GetLogger().LogError(FormatMessage(message));
        }

        public static void LogError(string message, IEnumerable<IError> errors)
        {
            var allErrors = string.Join("\n ", errors);
            GetLogger().LogError(FormatMessage(message ));
        }

        private static string FormatMessage(string message)
        {
            return $"{_serviceID} {message}";
        }
    }
}
