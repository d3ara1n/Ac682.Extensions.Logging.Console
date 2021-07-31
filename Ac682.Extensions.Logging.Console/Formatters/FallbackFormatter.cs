using System;
using System.Collections.Generic;
using System.Globalization;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class FallbackFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return true;
        }

        public IEnumerable<ColoredUnit> Format(object obj, Type type, string format = null)
        {
            return new[]
            {
                new ColoredUnit((obj is IFormattable formattable)?formattable.ToString(format, CultureInfo.CurrentUICulture):obj.ToString(), foreground: ConsoleColor.Gray)
            };
        }
    }
}