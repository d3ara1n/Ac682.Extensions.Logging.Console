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

        public IEnumerable<ColoredUnit> Format(object obj, Type type, string format = null)
        {
            return new[]
            {
                new ColoredUnit(format?.ToUpper() switch
                {
                    "U" => obj.ToString()!.ToUpper(),
                    "L" => obj.ToString()!.ToLower(),
                    null or _ => obj.ToString()
                }, foreground: ConsoleColor.Magenta)
            };
        }
    }
}