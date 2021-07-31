using System;
using System.Collections.Generic;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class ColoredUnitFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(ColoredUnit);
        }

        public IEnumerable<ColoredUnit> Format(object obj, Type type, string format = null)
        {
            return new[]
            {
                ((ColoredUnit) obj)
            };
        }
    }
}