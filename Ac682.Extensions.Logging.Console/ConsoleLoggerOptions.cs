using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Ac682.Extensions.Logging.Console
{
    public class ConsoleLoggerOptions
    {
        public LogLevel MinimalLevel { get; set; } = LogLevel.Debug;
        [Obsolete]
        public string Template { get; set; } = "{date:yy:mm:dd} {time:HH:MM:SS} {level:4} {category:short} {message}";

        public IList<IObjectLoggingFormatter> Formatters { get; set; }
    }
}