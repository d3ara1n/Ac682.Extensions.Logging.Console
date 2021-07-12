using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class NumberFormatter: IObjectLoggingFormatter
    {
        private readonly IEnumerable<Type> numberTypes = new Type[]
        {
            typeof(Int16),
            typeof(Int32),
            typeof(Int64),
            typeof(Double),
            typeof(Single),
            typeof(Decimal)
        };
        public bool IsTypeAvailable(Type type)
        {
            return numberTypes.Any(x => type == x);
        }

        public IEnumerable<ColoredUnit> Format(object obj, Type type)
        {
            return new[]
            {
                new ColoredUnit(obj.ToString(), foreground: ConsoleColor.DarkYellow)
            };
        }
    }
}