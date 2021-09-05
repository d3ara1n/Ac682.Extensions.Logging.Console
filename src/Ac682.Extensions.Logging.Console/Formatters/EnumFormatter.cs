using System;
using System.Collections.Generic;
using Spectre.Console;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class EnumFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type.IsEnum;
        }

        public string Format(object obj, Type type, string format = null)
        {
            format ??= "l";
            var line = format.ToLower() switch
            {
                "l" => $"{type.Name}.{obj}",
                "s" => obj.ToString(),
                _ => obj.ToString()
            };
            return $"[aqua]{line}[/]";
        }
    }
}