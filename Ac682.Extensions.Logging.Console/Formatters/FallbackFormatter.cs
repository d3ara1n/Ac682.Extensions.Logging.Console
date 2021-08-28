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

        public Markup Format(object obj, Type type, string format = null)
        {
            return
                new Markup(
                    (obj is IFormattable formattable)
                        ? formattable.ToString(format, CultureInfo.CurrentUICulture)
                        : obj.ToString(), new Style(Color.Silver));
        }
    }
}