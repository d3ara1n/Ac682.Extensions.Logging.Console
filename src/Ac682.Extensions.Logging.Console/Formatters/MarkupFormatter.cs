using System;
using System.Collections.Generic;
using Spectre.Console;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class MarkupFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(Markup);
        }

        public Markup Format(object obj, Type type, string format = null)
        {
            return ((Markup)obj);
        }
    }
}