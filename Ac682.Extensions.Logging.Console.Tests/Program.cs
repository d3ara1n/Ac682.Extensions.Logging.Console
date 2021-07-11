using System;
using Microsoft.Extensions.Logging;

namespace Ac682.Extensions.Logging.Console.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new ConsoleLoggerOptions()
            {
                MinimalLevel = LogLevel.Information
            };
            var provider = new ConsoleLoggerProvider(options);

            var logger = provider.CreateLogger(nameof(Program));
            logger.LogInformation("Hello, {}!", "World");
        }
    }
}