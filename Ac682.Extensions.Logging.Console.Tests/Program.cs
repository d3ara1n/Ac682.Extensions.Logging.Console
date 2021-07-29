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
            logger.LogInformation("Hello, World!");
            logger.LogInformation("I am {}, your master", "master");
            logger.LogInformation("Now is {}. And I am {}", DateTime.Now, ConsoleColor.Blue);
            logger.LogInformation("Hi {}, which is {}", new byte[] {0, 127, 255}, false);
            logger.LogError(new ArgumentNullException(nameof(args)), "Get out");
        }
    }
}