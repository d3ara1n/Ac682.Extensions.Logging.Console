using System;
using System.Collections;
using System.Collections.Generic;

namespace Ac682.Extensions.Logging.Console
{
    public interface IObjectLoggingFormatter
    {
        bool IsTypeAvailable(Type type);
        IEnumerable<ColoredUnit> Format(object obj, Type type);
    }
}