using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ac682.Extensions.Logging.Console
{
    public class ConsoleLoggerProvider : ILoggerProvider
    {
        private readonly ConsoleLoggerOptions _options;
        private readonly ConcurrentDictionary<string, ILogger> loggers = new ConcurrentDictionary<string, ILogger>();

        public ConsoleLoggerProvider(ConsoleLoggerOptions options)
        {
            _options = options;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName,
                name => new ConsoleLogger(name, _options));
        }

        public void Dispose()
        {
            loggers.Clear();
        }
    }
}