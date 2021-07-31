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

        public IEnumerable<ColoredUnit> Format(object obj, Type type, string format = null)
        {
            format ??= "L";
            return new[]
            {
                new ColoredUnit(format.ToUpper() switch
                {
                    "L" => $"{type.Name}.{obj}",
                    "S" => obj.ToString(),
                    _ => obj.ToString()
                }, foreground: ConsoleColor.DarkCyan)
            };
        }
    }
}