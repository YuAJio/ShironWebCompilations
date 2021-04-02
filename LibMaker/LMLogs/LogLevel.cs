using System;
using System.Collections.Generic;
using System.Text;

namespace LMLogs
{
    /// <summary>
    /// 日志等级
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 默认
        /// </summary>
        Default = 0,
        /// <summary>
        /// 文本信息
        /// </summary>
        Info = 1,
        /// <summary>
        /// 警告
        /// </summary>
        Warning = 2,
        /// <summary>
        /// 错误
        /// </summary>
        Error = 3,
        /// <summary>
        /// 异常
        /// </summary>
        Exception = 4,

    }
}
