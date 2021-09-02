using System;
using System.Collections.Generic;
using Spectre.Console;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class DateTimeFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(DateTime);
        }

        public Markup Format(object obj, Type type, string format = null)
        {
            return new Markup(format switch
            {
                null => obj.ToString(),
                _ => ((DateTime)obj).ToString(format)
            }, new Style(Color.Blue));
        }
    }
}