using System;
using System.Collections.Generic;
using System.Globalization;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class ByteFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(byte);
        }

        public IEnumerable<ColoredUnit> Format(object obj, Type type, string format = null)
        {
            format ??= "X2";
            return new[]
            {
                new ColoredUnit($"0x{((IFormattable)obj).ToString(format, CultureInfo.CurrentCulture)}", foreground:ConsoleColor.DarkYellow)
            };
        }
    }
}