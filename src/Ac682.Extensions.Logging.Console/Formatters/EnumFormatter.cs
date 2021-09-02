using System;
using System.Collections.Generic;
using Spectre.Console;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class EnumFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type.IsEnum;
        }

        public Markup Format(object obj, Type type, string format = null)
        {
            format ??= "L";
            return new Markup(format.ToUpper() switch
                {
                    "L" => $"{type.Name}.{obj}",
                    "S" => obj.ToString(),
                    _ => obj.ToString()
                }, new Style(Color.Cyan1));
        }
    }
}