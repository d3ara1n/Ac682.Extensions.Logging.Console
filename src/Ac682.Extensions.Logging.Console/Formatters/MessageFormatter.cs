using System;
using Ac682.Extensions.Logging.Console.Internal;
using Spectre.Console;

namespace Ac682.Extensions.Logging.Console.Formatters
{
    public class MessageFormatter: IObjectLoggingFormatter
    {
        public bool IsTypeAvailable(Type type)
        {
            return type == typeof(Message);
        }

        public string Format(object obj, Type type, string format = null)
        {
            return ((Message)obj).Text;
        }
    }
}