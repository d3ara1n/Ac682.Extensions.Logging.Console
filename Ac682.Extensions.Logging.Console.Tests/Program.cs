using System;
using System.Collections.Generic;
using Ac682.Extensions.Logging.Console.Formatters;
using Microsoft.Extensions.Logging;

namespace Ac682.Extensions.Logging.Console.Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConsoleLoggerOptionsBuilder();
            builder.AddBuiltinFormatters();
            var provider = new ConsoleLoggerProvider(builder.Build());

            var logger = provider.CreateLogger(nameof(Program));
            logger.LogInformation("Hello, {}! Now is {}. And I am {}", "World", DateTime.Now, ConsoleColor.Blue);
            logger.LogInformation("Hi {}, which is {}", new byte[] {0, 127, 255}, false);
        }
    }
}