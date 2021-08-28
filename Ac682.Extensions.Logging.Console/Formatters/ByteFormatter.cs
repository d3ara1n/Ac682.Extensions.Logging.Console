using System;
using System.Collections.Generic;
using System.Globalization;
using Spectre.Console;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class ByteFormatter : IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(byte);
        }

        public Markup Format(object obj, Type type, string format = null)
        {
            format ??= "X2";
            return new Markup($"0x{((IFormattable)obj).ToString(format, CultureInfo.CurrentCulture)}",
                new Style(Color.Gold1));
        }
    }
}