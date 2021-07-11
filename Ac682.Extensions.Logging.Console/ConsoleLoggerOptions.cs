using Microsoft.Extensions.Logging;

namespace Ac682.Extensions.Logging.Console
{
    public class ConsoleLoggerOptions
    {
        public LogLevel MinimalLevel { get; set; } = LogLevel.Debug;
    }
}