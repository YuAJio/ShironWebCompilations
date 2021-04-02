using NLog;
using System;

namespace LMLogs
{
    public class NLog
    {
        private readonly Logger logger;

        private NLog(Logger logger) { this.logger = logger; }

        public static NLog Logger { get; private set; }
        static NLog()
        {
            Logger = new NLog(LogManager.GetCurrentClassLogger());
        }

        public void Log(LogLevel level, object msg)
        {
            switch (level)
            {
                case LogLevel.Default:
                    logger.Trace(msg);
                    break;
                case LogLevel.Info:
                    logger.Info(msg);
                    break;
                case LogLevel.Warning:
                    logger.Warn(msg);
                    break;
                case LogLevel.Error:
                    logger.Error(msg);
                    break;
                case LogLevel.Exception:
                    logger.Error(msg);
                    break;
            }

        }

        public void Log(LogLevel level, string key, object msg)
        {
            switch (level)
            {
                case LogLevel.Default:
                    logger.Trace($"{key}--------{msg}");
                    break;
                case LogLevel.Info:
                    logger.Info($"{key}--------{msg}");
                    break;
                case LogLevel.Warning:
                    logger.Warn($"{key}--------{msg}");
                    break;
                case LogLevel.Error:
                    logger.Error($"{key}--------{msg}");
                    break;
                case LogLevel.Exception:
                    logger.Error($"{key}--------{msg}");
                    break;
            }
        }

    }
}
