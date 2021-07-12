using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using SConsole = System.Console;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace Ac682.Extensions.Logging.Console
{
    public class ConsoleLogger: ILogger
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
            return (int) logLevel >= (int) _minimalLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            lock (locker)
            {
                var levelName = logLevel switch
                {
                    LogLevel.Trace => "TRAC",
                    LogLevel.Debug => "DEBG",
                    LogLevel.Information => "INFO",
                    LogLevel.Warning => "WARN",
                    LogLevel.Error => "ERRO",
                    LogLevel.Critical => "CRIT",
                    _ => "NONE"
                };
                
                var datetime = DateTime.Now;
                var category = (_name.Contains('.') ? _name.Substring(_name.LastIndexOf('.') + 1) : _name);
                // 20/07/22 00:11 DEBG NAME => STHSTH
                // SConsole.ResetColor();
                // SConsole.ForegroundColor = ConsoleColor.Blue;
                // SConsole.Write(datetime);
                // SConsole.ForegroundColor = color.Item2;
                // SConsole.Write($"{levelName}");
                // SConsole.ResetColor();
                // SConsole.ForegroundColor = ConsoleColor.DarkCyan;
                // SConsole.Write($" {category} ");

                List<object> properties = new List<object>();
                
                properties.Add(datetime);
                properties.Add(" ");
                properties.Add(logLevel);
                properties.Add(" ");
                
                if (state is IEnumerable<KeyValuePair<string, object>> states)
                {
                    const string KEY = "{OriginalFormat}";
                    var format = states.FirstOrDefault(x => x.Key == KEY).Value.ToString();
                    var args = states.Where(x => x.Key != KEY).Select(x => x.Value).ToList();

                    int index = -1;
                    int lastIndex = -1;
                    int count = 0;
                    while ((index = format!.IndexOf("{}", index + 1, StringComparison.Ordinal)) != -1)
                    {
                        properties.Add(format[(lastIndex == -1 ? 0: lastIndex )..index]);
                        
                        properties.Add(args[count]);

                        lastIndex = index + 2;
                        count++;
                    }

                    if(lastIndex == -1)
                    {
                        properties.Add(format);
                    }
                    
                }

                foreach (var prop in properties)
                {
                    if (prop is IEnumerable enu && !(prop is IEnumerable<char>))
                    {
                        var list = enu.Cast<object>().ToList();
                        Write(new ColoredUnit("[", foreground: ConsoleColor.DarkGray));
                        int count = list.Count;
                        for (int i = 0; i < count; i++)
                        {
                            Write(list[i]);
                            if(i < count - 1) Write(new ColoredUnit(", ", foreground:ConsoleColor.DarkGray));
                        }
                        Write(new ColoredUnit("]", foreground: ConsoleColor.DarkGray));
                    }
                    else
                    {
                        Write(prop);
                    }
                }
                SConsole.WriteLine();
            }
        }

        private void Write(object obj)
        {
            IObjectLoggingFormatter inst = null;
            var form = _formatters.FirstOrDefault(x =>
            {
                inst = Activator.CreateInstance(x) as IObjectLoggingFormatter;
                return inst!.IsTypeAvailable(obj.GetType());
            });
            var units = inst.Format(obj, obj.GetType()) ?? Enumerable.Empty<ColoredUnit>();

            foreach (var unit in units)
            {
                SConsole.ForegroundColor = unit.Foreground;
                SConsole.BackgroundColor = unit.Background;
                SConsole.Write(unit.Text);
            }
        }
    }
}