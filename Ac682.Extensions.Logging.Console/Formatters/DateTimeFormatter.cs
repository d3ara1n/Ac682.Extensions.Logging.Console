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

        public IEnumerable<ColoredUnit> Format(object obj, Type type)
        {
            return new ColoredUnit[]
            {
                new ColoredUnit(obj.ToString(), foreground: ConsoleColor.Blue)
            };
        }
    }
}