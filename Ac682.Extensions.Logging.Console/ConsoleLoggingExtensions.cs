using System;
using Microsoft.Extensions.Logging;

namespace Ac682.Extensions.Logging.Console
{
    public static class ConsoleLoggingExtensions
    {
        public static ILoggingBuilder AddConsole(this ILoggingBuilder builder)
        {
            return AddConsole(builder, new ConsoleLoggerOptions {MinimalLevel = LogLevel.Trace});
        }

        public static ILoggingBuilder AddConsole(this ILoggingBuilder builder, ConsoleLoggerOptions options)
        {
            return builder.AddProvider(new ConsoleLoggerProvider(options));
        }

        public static ILoggingBuilder AddConsole(this ILoggingBuilder builder,
            Action<ConsoleLoggerOptionsBuilder> configure)
        {
            var optionsBuilder = new ConsoleLoggerOptionsBuilder();
            configure(optionsBuilder);
            return builder.AddConsole(optionsBuilder.Build());
        }
    }
}