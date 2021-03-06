using System;
using System.Collections.Generic;
using Spectre.Console;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class DateTimeFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(DateTime);
        }

        public string Format(object obj, Type type, string format = null)
        {
            var line = format switch
            {
                null => obj.ToString(),
                _ => ((DateTime)obj).ToString(format)
            };
            return $"[blue]{line}[/]";
        }
    }
}