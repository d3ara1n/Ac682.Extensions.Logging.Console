using System;
using System.Collections.Generic;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class ExceptionFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(Exception);
        }

        public IEnumerable<ColoredUnit> Format(object obj, Type type, string format = null)
        {
            format ??= "FULL";
            return new[]
            {
                new ColoredUnit(format switch
                {
                    "MESSAGE" => ((Exception)obj).Message,
                    "FULL" or _ => obj.ToString()
                })
            };
        }
    }
}