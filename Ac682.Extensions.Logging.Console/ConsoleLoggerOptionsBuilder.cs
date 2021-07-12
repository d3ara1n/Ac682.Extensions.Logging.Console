using System;
using System.Collections.Generic;
using System.Linq;
using Ac682.Extensions.Logging.Console.Formatters;
using Microsoft.Extensions.Logging;

namespace Ac682.Extensions.Logging.Console
{
    public class ConsoleLoggerOptionsBuilder
    {
        List<Type> formatters = new();
        LogLevel minimalLevel = LogLevel.Information;
        
        public ConsoleLoggerOptions Build()
        {
            if (formatters.All(x => x != typeof(FallbackFormatter)))
            {
                formatters.Add(typeof(FallbackFormatter));
            }
            
            var options = new ConsoleLoggerOptions()
            {
                Formatters = formatters,
                MinimalLevel = minimalLevel
            };
            return options;
        }

        public ConsoleLoggerOptionsBuilder AddFormatter(Type formatterType)
        {
            formatters.Add(formatterType);
            return this;
        }

        public ConsoleLoggerOptionsBuilder SetMinimalLevel(LogLevel level)
        {
            minimalLevel = level;
            return this;
        }
    }
}