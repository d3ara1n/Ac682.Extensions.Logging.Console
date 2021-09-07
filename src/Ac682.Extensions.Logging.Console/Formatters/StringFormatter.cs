using System;
using System.Collections.Generic;
using Spectre.Console;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class StringFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(string);
        }

        public string Format(object obj, Type type, string format = null)
        {
            return $"[bold white]{Markup.Escape(obj.ToString()!)}[/]";
        }
    }
}