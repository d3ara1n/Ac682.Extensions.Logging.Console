using System;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class TimeSpanFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(TimeSpan);
        }

        public string Format(object obj, Type type, string format = null)
        {
            var line = format switch
            {
                null => obj.ToString(),
                _ => ((TimeSpan)obj).ToString(format)
            };
            return $"[blue]{line}[/]";
        }
    }
}