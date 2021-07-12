using System;
using System.Linq.Expressions;

namespace Ac682.Extensions.Logging.Console
{
    public struct ColoredUnit
    {
        public ConsoleColor Foreground { get; set; }
        public ConsoleColor Background { get; set; }
        public string Text { get; set; }
        
        public ColoredUnit(string text, ConsoleColor foreground = ConsoleColor.Gray, ConsoleColor background = ConsoleColor.Black)
        {
            Foreground = foreground;
            Background = background;
            Text = text;
        }
    }
}