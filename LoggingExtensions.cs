using System;
using NLog;

namespace SpatialDotNet
{
    internal static class LoggingExtensions
    {
        public static void Log(this Logger log, SpatialLogLevel level, DateTime time, string message)
        {
            log.Log(SpatialToLogLevel(level), $"{time}: {message}");
        }

        private static LogLevel SpatialToLogLevel(SpatialLogLevel level)
        {
            switch (level)
            {
                case SpatialLogLevel.Debug:
                    return LogLevel.Debug;

                case SpatialLogLevel.Info:
                    return LogLevel.Info;
                    
                case SpatialLogLevel.Warning:
                    return LogLevel.Warn;

                case SpatialLogLevel.Error:
                    return LogLevel.Error;
                    
                case SpatialLogLevel.Fatal:
                    return LogLevel.Fatal;

                case SpatialLogLevel.Panic:
                    return LogLevel.Error;
            }

            return LogLevel.Off;
        }
    }
}