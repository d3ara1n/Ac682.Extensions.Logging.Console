using System;
using Microsoft.Extensions.Logging;

namespace Ac682.Extensions.Logging.Console.Tests
{
    internal static class Program
    {
        private static void Main()
        {
            var builder = new ConsoleLoggerOptionsBuilder();
            builder.AddBuiltinFormatters();
            var provider = new ConsoleLoggerProvider(builder.Build());

            var logger = provider.CreateLogger(nameof(Program));
            logger.LogInformation("Hello, World!");
            logger.LogInformation("I am {Master}({Version}), your master", 123, typeof(Program).Assembly.GetName().Version);
            logger.LogInformation("Now is {NowDateTime}. And I am {Color}", DateTime.Now, ConsoleColor.Blue);
            logger.LogInformation("Hi {Bytes}, which is {Boolean}", new byte[] {0, 127, 255}, false);
            logger.LogError(new ArgumentNullException(nameof(Main)), "Get out");
        }
    }
}