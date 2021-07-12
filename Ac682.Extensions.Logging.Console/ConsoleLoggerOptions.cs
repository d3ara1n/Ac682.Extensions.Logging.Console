using System;
using System.Collections;
using System.Collections.Generic;
using Ac682.Extensions.Logging.Console.Formatters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ac682.Extensions.Logging.Console
{
    public class ConsoleLoggerOptions
    {
        public LogLevel MinimalLevel { get; set; } = LogLevel.Debug;
        [Obsolete]
        public string Template { get; set; } = "{date} {time} {level} {message}";
        public IEnumerable<Type> Formatters { get; set; } = new[] {typeof(StringFormatter), typeof(FallbackFormatter)};
    }
}