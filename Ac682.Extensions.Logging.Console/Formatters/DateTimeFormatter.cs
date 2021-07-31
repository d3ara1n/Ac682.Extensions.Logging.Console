using System;
using System.Collections.Generic;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class DateTimeFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(DateTime);
        }

        public IEnumerable<ColoredUnit> Format(object obj, Type type, string format = null)
        {
            return new[]
            {
                new ColoredUnit(format switch
                {
                    null => obj.ToString(),
                    _ => ((DateTime)obj).ToString(format)
                }, foreground: ConsoleColor.Blue)
            };
        }
    }
}