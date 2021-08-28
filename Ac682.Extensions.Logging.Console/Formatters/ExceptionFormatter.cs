using System;
using System.Collections.Generic;
using Spectre.Console;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class ExceptionFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(Exception);
        }

        public Markup Format(object obj, Type type, string format = null)
        {
            format ??= "FULL";
            return
                new Markup(format switch
                {
                    "MESSAGE" => ((Exception)obj).Message,
                    "FULL" or _ => obj.ToString()
                }, new Style(Color.Silver));
        }
    }
}