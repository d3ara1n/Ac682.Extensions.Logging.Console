using System;
using System.Collections.Generic;
using Spectre.Console;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class ExceptionFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type.IsAssignableTo(typeof(Exception));
        }

        public string Format(object obj, Type type, string format = null)
        {
            format ??= "full";
            var ex = (Exception)obj;
            var line = format.ToLower() switch
            {
                "message" => $"[red]{ex.Message}[/]",
                "full" or _ => $"[red]{ex.Message}[/]\n[silver]{ex.StackTrace}[/]"
            };
            return line;
        }
    }
}