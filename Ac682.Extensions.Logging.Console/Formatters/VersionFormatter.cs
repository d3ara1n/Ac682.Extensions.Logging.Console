using System;
using System.Collections.Generic;
using Spectre.Console;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class VersionFormatter : IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(Version);
        }
        
        public Markup Format(object obj, Type type, string format = null)
        {
            return new Markup(obj.ToString(), new Style(Color.Yellow));
        }
    }
}