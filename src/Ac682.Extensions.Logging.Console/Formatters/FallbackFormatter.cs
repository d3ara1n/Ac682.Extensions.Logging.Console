using System;
using System.Collections.Generic;
using System.Globalization;
using Spectre.Console;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class FallbackFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return true;
        }

        public string Format(object obj, Type type, string format = null)
        {
            return $"[silver]{obj}[/]";
        }
    }
}