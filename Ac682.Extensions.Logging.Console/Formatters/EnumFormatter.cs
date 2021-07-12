using System;
using System.Collections.Generic;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class EnumFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type.IsEnum;
        }

        public IEnumerable<ColoredUnit> Format(object obj, Type type)
        {
            return new[]
            {
                new ColoredUnit($"{type.Name}.{obj}", foreground: ConsoleColor.DarkCyan)
            };
        }
    }
}