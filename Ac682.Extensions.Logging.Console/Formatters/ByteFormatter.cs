using System;
using System.Collections.Generic;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class ByteFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(byte);
        }

        public IEnumerable<ColoredUnit> Format(object obj, Type type)
        {
            return new[]
            {
                new ColoredUnit($"0x{obj:X2}", foreground:ConsoleColor.DarkYellow)
            };
        }
    }
}