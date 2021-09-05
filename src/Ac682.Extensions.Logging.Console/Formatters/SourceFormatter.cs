using System;
using Ac682.Extensions.Logging.Console.Internal;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class SourceFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(Source);
        }

        public string Format(object obj, Type type, string format = null)
        {
            format ??= "s";
            var name = ((Source)obj).FullName;
            var line = format.ToLower() switch
            {
                "s" => name[(name.LastIndexOf('.') + 1) ..],
                "l" => name,
                _ => name
            };
            return $"[teal]{line}[/]";
        }
    }
}