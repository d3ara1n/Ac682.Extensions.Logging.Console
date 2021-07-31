using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Ac682.Extensions.Logging.Console
{
    public interface IObjectLoggingFormatter
    {
        /// <summary>
        /// 判断类型是否能被该实例格式化
        /// </summary>
        /// <param name="type">需要判断的类型</param>
        /// <returns>判断结果</returns>
        bool IsTypeAvailable(Type type);
        
        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="obj">被格式化的对象实例</param>
        /// <param name="type">其类型</param>
        /// <param name="format">字符串表示的格式</param>
        /// <returns>用户 <see cref="ColoredUnit"/> 表示的可打印结果</returns>
        IEnumerable<ColoredUnit> Format(object obj, Type type, string format = null);
    }
}