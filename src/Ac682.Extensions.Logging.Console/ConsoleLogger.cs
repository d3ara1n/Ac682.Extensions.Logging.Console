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
using Spectre.Console;

namespace Ac682.Extensions.Logging.Console
{
    public class ConsoleLogger : ILogger
    {
        private static readonly object locker = new object();
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


        private static readonly Regex formatRegex = new Regex(@"\{(?<name>[A-Za-z0-9_.]*)[:]?(?<format>[0-9a-zA-Z-_+.:]*)??\}");
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

                    
                    var matches = formatRegex.Matches(format);
                    var addedLength = 0;
                    if (matches.Count > 0)
                    {
                        for(var count = 0; count < matches.Count; count++)
                        {
                            properties.Add((new Markup(format[addedLength..matches[count].Index], new Style(Color.Silver)), null));
                            var group = matches[count].Groups["format"];
                            properties.Add((args[count], group.Value.Length > 0? group.Value: null));
                            addedLength = matches[count].Index + matches[count].Length;
                        }

                        if (format.Length > addedLength)
                        {
                            properties.Add((new Markup(format[addedLength..], new Style(Color.Silver)), null));
                        }
                    }
                    else
                    {
                        properties.Add((new Markup(format, new Style(Color.Silver)), null));
                    }

                }

                if (exception != null)
                {
                    properties.Add(("\n", null));
                    properties.Add((exception, null));
                }
                
                properties.Add(("\n", null));
                
                WriteAll(properties);
            }
        }

        private void WriteAll(IEnumerable<(object, string)> properties)
        {
            foreach (var (prop, format) in properties)
            {
                if (prop is IEnumerable enu && prop is not IEnumerable<char>)
                {
                    var list = enu.Cast<object>().ToList();
                    Write(new Markup("[[", new Style(Color.Grey)), format);
                    int count = list.Count;
                    for (int i = 0; i < count; i++)
                    {
                        Write(list[i], format);
                        if (i < count - 1) Write(new Markup(", ", new Style(Color.Grey)), format);
                    }
                    Write(new Markup("]]", new Style(Color.Grey)), format);
                }
                else
                {
                    Write(prop, format);
                }
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
            var markup = inst.Format(obj, obj.GetType(), format);

            if (markup != null)
            {
                AnsiConsole.Render(markup);
            }
        }
    }
}