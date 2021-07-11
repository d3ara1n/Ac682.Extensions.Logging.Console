using System;
using System.Collections;
using System.Collections.Generic;
using SConsole = System.Console;
using Microsoft.Extensions.Logging;

namespace Ac682.Extensions.Logging.Console
{
    public class ConsoleLogger: ILogger
    {
    private static readonly object locker = new object();
        private readonly LogLevel _minimalLevel;
        private readonly string _name;

        public ConsoleLogger(string name, LogLevel minimalLevel = LogLevel.Debug)
        {
            _name = name;
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
                
                var datetime = DateTime.Now.ToString("yy/MM/dd HH:mm:ss ");
                
                var color = GetColor(logLevel);
                if (color.Item1 != ConsoleColor.Black) SConsole.BackgroundColor = color.Item1;
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
                // SConsole.ForegroundColor = ConsoleColor.White;
                // SConsole.WriteLine(formatter(state, exception));

                List<object> properties = new List<object>();
                if (state is IEnumerable<KeyValuePair<string, object>>)
                {
                    //TODO: 明天接着干
                }
            }
        }

        private (ConsoleColor, ConsoleColor) GetColor(LogLevel level)
        {
            return level switch
            {
                LogLevel.Trace => (ConsoleColor.Black, ConsoleColor.Cyan),
                LogLevel.Debug => (ConsoleColor.Black, ConsoleColor.DarkMagenta),
                LogLevel.Information => (ConsoleColor.Black, ConsoleColor.Green),
                LogLevel.Warning => (ConsoleColor.Black, ConsoleColor.Yellow),
                LogLevel.Error => (ConsoleColor.Black, ConsoleColor.Red),
                LogLevel.Critical => (ConsoleColor.Red, ConsoleColor.White),
                _ => (ConsoleColor.Black, ConsoleColor.Gray)
            };
        }
    }
}