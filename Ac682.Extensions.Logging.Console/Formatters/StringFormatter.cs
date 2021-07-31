using System;
using System.Collections.Generic;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class StringFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(string);
        }

        public IEnumerable<ColoredUnit> Format(object obj, Type type, string format = null)
        {
            return new[]
            {
                new ColoredUnit()
                {
                    Foreground = ConsoleColor.White,
                    Text = obj.ToString()
                }
            };
        }
    }
}