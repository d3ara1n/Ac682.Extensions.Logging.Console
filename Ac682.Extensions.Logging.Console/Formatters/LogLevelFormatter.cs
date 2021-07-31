using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class LogLevelFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(LogLevel);
        }

        public IEnumerable<ColoredUnit> Format(object obj, Type type, string format = null)
        {
            format ??= "N4";
            var name = GetLogLevelName(((LogLevel) obj));

            var regex = new Regex("^(?<format>[ULN]??)(?<length>[0-9]??)$");
            var match = regex.Match(format.ToUpper());
            var gFormat = match.Groups["format"];
            var gLength = match.Groups["length"];
            if (gFormat.Length > 0)
            {
                name = gFormat.Value switch
                {
                    "U" => name.ToUpper(),
                    "L" => name.ToLower(),
                    "N" or _ => name
                };
            }

            if (gLength.Length > 0 && int.TryParse(gLength.Value, out int length) && length < name.Length)
            {
                name = name.Substring(0, length);
            }
            
            return new[]
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
            return level.ToString();
        }
    }
}