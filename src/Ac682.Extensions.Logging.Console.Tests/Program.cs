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

            var logger = provider.CreateLogger(typeof(Program).FullName);
            logger.LogInformation("[blue] I AM BLUE, YOUR ARE {}[/]","[yellow]NOT[/]");
            logger.LogDebug("Hello, World!");
            logger.LogInformation("I am {Master}({Version}), your {Name}", 123, typeof(Program).Assembly.GetName().Version, "master");
            logger.LogWarning("Now is {NowDateTime}. And I am {Color}", DateTime.Now, ConsoleColor.Blue);
            logger.LogError("Hi {Bytes}, which is {Boolean}", new byte[] {0, 127, 255}, false);
            logger.LogCritical(new ArgumentNullException(nameof(Main)), "Get out");
        }
    }
}