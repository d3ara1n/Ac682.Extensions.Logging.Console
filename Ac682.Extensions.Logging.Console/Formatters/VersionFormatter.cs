using System;
using System.Collections.Generic;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class VersionFormatter : IObjectLoggingFormatter
    {
        public IEnumerable<ColoredUnit> Format(object obj, Type type, string format = null)
        {
            return new []
            {
                new ColoredUnit(obj.ToString(), foreground:ConsoleColor.DarkYellow)
            };
        }

        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(Version);
        }
    }
}