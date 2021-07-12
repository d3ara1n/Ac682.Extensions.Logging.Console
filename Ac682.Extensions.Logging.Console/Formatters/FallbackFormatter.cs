using System;
using System.Collections.Generic;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class FallbackFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return true;
        }

        public IEnumerable<ColoredUnit> Format(object obj, Type type)
        {
            return new[]
            {
                new ColoredUnit(obj.ToString(), foreground: ConsoleColor.Gray)
            };
        }
    }
}