using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SConsole = System.Console;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using Spectre.Console;
using Ac682.Extensions.Logging.Console.Internal;

namespace Ac682.Extensions.Logging.Console
{
    public class ConsoleLogger : ILogger
    {
        private static readonly object locker = new object();
        private readonly string _name;
        private readonly ConsoleLoggerOptions _options;

        internal Action<string> WriteFunction = str =>
        {
            lock (locker)
            {
                AnsiConsole.Markup(str);
            }
        };

        public ConsoleLogger(string name, ConsoleLoggerOptions options)
        {
            _name = name;
            _options = options;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (int)logLevel >= (int)_options.MinimalLevel;
        }


        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            var datetime = DateTime.Now;
            //var properties = new List<(object,string)> {(datetime, null), (" ", null), (logLevel, "U4"), (" ", null)};


            var properties = new Dictionary<string, object>()
            {
                {
                    "Time", datetime.TimeOfDay
                },
                {
                    "Date", datetime.Date
                },
                {
                    "DateTime", datetime
                },
                {
                    "Level", logLevel
                },
                {
                    "Source", new Source(_name)
                }
            };
            if (state is IEnumerable<KeyValuePair<string, object>> states)
            {
                properties.Add("Message", new Message(BuildMessage(states)));
            }

            if (exception != null)
            {
                properties.Add("Exception", exception);
            }

            var message = RenderTemplate(_options.Template, properties, FormatOne);
            WriteFunction?.Invoke(message);
        }

        private static readonly Regex formatRegex =
            new Regex(@"\{(?<key>[A-Za-z0-9_.|]*)[:]?(?<format>[0-9a-zA-Z-_+.:]*)??\}");

        private string RenderTemplate(string template, IDictionary<string, object> properties,
            Func<object, string, string> formatter)
        {
            var matches = formatRegex.Matches(template);
            var builder = new StringBuilder();
            var addedLength = 0;
            if (matches.Count > 0)
            {
                for (var count = 0; count < matches.Count; count++)
                {
                    var key = matches[count].Groups["key"].Value;
                    var format = matches[count].Groups["format"].Value;
                    var obj = properties.ContainsKey(key) ? properties[key] : null;
                    var pre = template[addedLength..matches[count].Index];
                    if (string.IsNullOrWhiteSpace(pre)) builder.Append(pre);
                    else builder.Append($"[silver]{pre}[/]");

                    if (obj == null && key.Contains('|'))
                    {
                        var keys = key.Split('|');
                        foreach (var skey in keys)
                        {
                            obj = properties.ContainsKey(skey) ? properties[skey] : null;
                            if(obj != null) break;
                        }
                    }
                    if (obj != null) builder.Append(formatter(obj, format.Length > 0 ? format : null));
                    addedLength = matches[count].Index + matches[count].Length;
                }

                if (template.Length > addedLength)
                {
                    builder.Append($"[silver]{template[addedLength..]}[/]");
                }
            }
            else
            {
                builder.Append($"[silver]{template}[/]");
            }

            return builder.ToString();
        }


        private string BuildMessage(IEnumerable<KeyValuePair<string, object>> states)
        {
            const string KEY = "{OriginalFormat}";
            var format = states.First(x => x.Key == KEY).Value.ToString();
            var args = states.Where(x => x.Key != KEY).Select(x => x.Value).ToList();
            // make message dictionary
            var elements = new Dictionary<string, object>();
            var matches = formatRegex.Matches(format);
            if (matches.Count > 0)
            {
                for (var i = 0; i < matches.Count; i++)
                {
                    if (elements.ContainsKey(matches[i].Groups["key"].Value)) elements[matches[i].Groups["key"].Value] = args[i];
                    else elements.Add(matches[i].Groups["key"].Value, args[i]);
                }
            }

            return RenderTemplate(format, elements, FormatOne);
        }

        private string FormatOne(object obj, string format)
        {
            if (obj is not string && obj is IEnumerable enumerable)
            {
                var builder = new StringBuilder();
                builder.Append("[grey][[[/]");
                builder.Append(string.Join("[grey], [/]", enumerable.Cast<object>().Select(x => FormatOne(x, null))));
                builder.Append("[grey]]][/]");

                return builder.ToString();
            }
            else
            {
                IObjectLoggingFormatter inst = null;
                var form = _options.Formatters.FirstOrDefault(x =>
                {
                    inst = Activator.CreateInstance(x) as IObjectLoggingFormatter;
                    return inst!.IsTypeAvailable(obj.GetType());
                });
                var markup = inst.Format(obj, obj.GetType(), format);

                return markup;
            }
        }
    }
}