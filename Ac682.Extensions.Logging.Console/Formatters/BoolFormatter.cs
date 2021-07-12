using System;
using System.Collections.Generic;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class BoolFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(bool);
        }

        public IEnumerable<ColoredUnit> Format(object obj, Type type)
        {
            return new[]
            {
                new ColoredUnit(obj.ToString(), foreground: ConsoleColor.Magenta)
            };
        }
    }
}