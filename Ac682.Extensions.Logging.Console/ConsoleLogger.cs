using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SConsole = System.Console;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Ac682.Extensions.Logging.Console
{
    public class ConsoleLogger : ILogger
    {
        private readonly object locker = new object();
        private readonly LogLevel _minimalLevel;
        private readonly string _name;
        private readonly IEnumerable<Type> _formatters;

        public ConsoleLogger(string name, IEnumerable<Type> formatters, LogLevel minimalLevel = LogLevel.Debug)
        {
            _name = name;
            _formatters = formatters;
            _minimalLevel = minimalLevel;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (int)logLevel >= (int)_minimalLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            lock (locker)
            {

                var datetime = DateTime.Now;
                var properties = new List<(object,string)> {(datetime, null), (" ", null), (logLevel, "U4"), (" ", null)};


                if (state is IEnumerable<KeyValuePair<string, object>> states)
                {
                    const string KEY = "{OriginalFormat}";
                    var format = states.First(x => x.Key == KEY).Value.ToString();
                    var args = states.Where(x => x.Key != KEY).Select(x => x.Value).ToList();

                    var regex = new Regex("{(?<name>[A-Za-z0-9_.]*)[:]?(?<format>[0-9a-zA-Z-_+.:]*)??}");
                    var matches = regex.Matches(format);
                    var addedLength = 0;
                    if (matches.Count > 0)
                    {
                        for(var count = 0; count < matches.Count; count++)
                        {
                            properties.Add((new ColoredUnit(format[addedLength..matches[count].Index]), null));
                            var group = matches[count].Groups["format"];
                            properties.Add((args[count], group.Value.Length > 0? group.Value: null));
                            addedLength = matches[count].Index + matches[count].Length;
                        }

                        if (format.Length > addedLength)
                        {
                            properties.Add((new ColoredUnit(format[addedLength..]), null));
                        }
                    }
                    else
                    {
                        properties.Add((new ColoredUnit(format), null));
                    }

                }

                if (exception != null)
                {
                    properties.Add(("\n", null));
                    properties.Add((exception, null));
                }

                foreach (var (prop, format) in properties)
                {
                    if (prop is IEnumerable enu && !(prop is IEnumerable<char>))
                    {
                        var list = enu.Cast<object>().ToList();
                        Write(new ColoredUnit("[", foreground: ConsoleColor.DarkGray), format);
                        int count = list.Count;
                        for (int i = 0; i < count; i++)
                        {
                            Write(list[i], format);
                            if (i < count - 1) Write(new ColoredUnit(", ", foreground: ConsoleColor.DarkGray), format);
                        }
                        Write(new ColoredUnit("]", foreground: ConsoleColor.DarkGray), format);
                    }
                    else
                    {
                        Write(prop, format);
                    }
                }
                SConsole.WriteLine();
            }
        }

        private void Write(object obj, string format)
        {
            IObjectLoggingFormatter inst = null;
            var form = _formatters.FirstOrDefault(x =>
            {
                inst = Activator.CreateInstance(x) as IObjectLoggingFormatter;
                return inst!.IsTypeAvailable(obj.GetType());
            });
            var units = inst.Format(obj, obj.GetType(), format) ?? Enumerable.Empty<ColoredUnit>();

            foreach (var unit in units)
            {
                SConsole.ResetColor();
                SConsole.ForegroundColor = unit.Foreground;
                if (unit.Background != ConsoleColor.Black) SConsole.BackgroundColor = unit.Background;
                SConsole.Write(unit.Text);
            }
        }
    }
}