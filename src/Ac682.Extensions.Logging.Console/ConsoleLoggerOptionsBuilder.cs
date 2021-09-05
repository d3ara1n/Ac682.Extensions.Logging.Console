using System;
using System.Collections.Generic;
using System.Linq;
using Ac682.Extensions.Logging.Console.Formatters;
using Microsoft.Extensions.Logging;

namespace Ac682.Extensions.Logging.Console
{
    public class ConsoleLoggerOptionsBuilder
    {
        ConsoleLoggerOptions options = new ();
        List<Type> formatters = new();

        public ConsoleLoggerOptions Build()
        {
            if (formatters.All(x => x != typeof(FallbackFormatter)))
            {
                formatters.Add(typeof(FallbackFormatter));
            }

            options.Formatters = formatters;
            return options;
        }

        public ConsoleLoggerOptionsBuilder AddFormatter(Type formatterType)
        {
            formatters.Add(formatterType);
            return this;
        }

        public ConsoleLoggerOptionsBuilder SetMinimalLevel(LogLevel level)
        {
            options.MinimalLevel = level;
            return this;
        }

        public ConsoleLoggerOptionsBuilder SetTemplate(string template)
        {
            options.Template = template;
            return this;
        }
    }
}