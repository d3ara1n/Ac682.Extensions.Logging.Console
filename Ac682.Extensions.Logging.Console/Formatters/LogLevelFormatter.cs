using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class LogLevelFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(LogLevel);
        }

        public IEnumerable<ColoredUnit> Format(object obj, Type type)
        {
            var name = GetLogLevelName(((LogLevel) obj));
            return new ColoredUnit[]
            {
                obj switch
                {
                    LogLevel.Trace => new ColoredUnit(name, foreground:ConsoleColor.Cyan),
                    LogLevel.Debug => new ColoredUnit(name, foreground: ConsoleColor.DarkMagenta),
                    LogLevel.Information => new ColoredUnit(name, foreground:ConsoleColor.Green),
                    LogLevel.Warning => new ColoredUnit(name, foreground:ConsoleColor.Yellow),
                    LogLevel.Error => new ColoredUnit(name, foreground:ConsoleColor.Red),
                    LogLevel.Critical => new ColoredUnit(name, foreground:ConsoleColor.White, background: ConsoleColor.Red),
                    _ => new ColoredUnit(name)
                },
            };
        }

        private string GetLogLevelName(LogLevel level)
        {
            return level switch
            {
                LogLevel.Trace => "TRAC",
                LogLevel.Debug => "DEBG",
                LogLevel.Information => "INFO",
                LogLevel.Warning => "WARN",
                LogLevel.Error => "ERRO",
                LogLevel.Critical => "CRIT",
                LogLevel.None => "NONE",
                _ => "UNKO"
            };
        }
    }
}