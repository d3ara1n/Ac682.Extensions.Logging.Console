using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Spectre.Console;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class LogLevelFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(LogLevel);
        }

        public string Format(object obj, Type type, string format = null)
        {
            format ??= "n4";
            var name = GetLogLevelName(((LogLevel) obj));

            var regex = new Regex("^(?<format>[ULNuln]??)(?<length>[0-9]??)$");
            var match = regex.Match(format.ToUpper());
            var gFormat = match.Groups["format"];
            var gLength = match.Groups["length"];
            if (gFormat.Length > 0)
            {
                name = gFormat.Value.ToLower() switch
                {
                    "u" => name.ToUpper(),
                    "l" => name.ToLower(),
                    "n" or _ => name
                };
            }

            if (gLength.Length > 0 && int.TryParse(gLength.Value, out int length) && length < name.Length)
            {
                name = name.Substring(0, length);
            }

            var color = obj switch
            {
                LogLevel.Trace => "cyan1",
                LogLevel.Debug => "darkmagenta",
                LogLevel.Information => "lime",
                LogLevel.Warning => "yellow",
                LogLevel.Error => "red",
                LogLevel.Critical => "white on red",
                _ => "white"
            };
            return $"[{color}]{name}[/]";
        }

        private string GetLogLevelName(LogLevel level)
        {
            return level.ToString();
        }
    }
}