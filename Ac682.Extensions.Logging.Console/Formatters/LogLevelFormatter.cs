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

        public Markup Format(object obj, Type type, string format = null)
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

            return obj switch
            {
                LogLevel.Trace => new Markup(name, new Style(Color.Cyan1)),
                LogLevel.Debug => new Markup(name, new Style(Color.DarkMagenta)),
                LogLevel.Information => new Markup(name, new Style(Color.Green)),
                LogLevel.Warning => new Markup(name, new Style(Color.Yellow)),
                LogLevel.Error => new Markup(name, new Style(Color.Red)),
                LogLevel.Critical => new Markup(name, new Style(Color.White, Color.Red)),
                _ => new Markup(name)
            };
        }

        private string GetLogLevelName(LogLevel level)
        {
            return level.ToString();
        }
    }
}