using System;
using Ac682.Extensions.Logging.Console.Formatters;

namespace Ac682.Extensions.Logging.Console
{
    public static class ConsoleLoggerOptionsBuilderExtensions
    {
        public static ConsoleLoggerOptionsBuilder AddFormatter<TFormatter>(this ConsoleLoggerOptionsBuilder builder)
        where TFormatter: IObjectLoggingFormatter
        {
            return builder.AddFormatter(typeof(TFormatter));
        }

        public static ConsoleLoggerOptionsBuilder AddBuiltinFormatters(this ConsoleLoggerOptionsBuilder builder)
        {
            return builder
                .AddFormatter<StringFormatter>()
                .AddFormatter<ColoredUnitFormatter>()
                .AddFormatter<NumberFormatter>()
                .AddFormatter<LogLevelFormatter>()
                .AddFormatter<EnumFormatter>()
                .AddFormatter<DateTimeFormatter>()
                .AddFormatter<ByteFormatter>()
                .AddFormatter<BoolFormatter>();
        }
    }
}