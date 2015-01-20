using System;
using System.Configuration;
using System.Runtime.InteropServices;

namespace NLogger
{
    /// <summary>
    /// Factory class for creating ILogger instances
    /// </summary>
    public static class LoggerFactory
    {
        /// <summary>
        /// Create a logger using App.config or Web.config settings
        /// </summary>
        /// <returns></returns>
        public static ILogger CreateLogger()
        {
            var config = (NLoggerSection)ConfigurationManager.GetSection("nLogger");
            return CreateLogger(config);
        }

        /// <summary>
        /// Create a logger using App.config or Web.config settings
        /// </summary>
        /// <returns></returns>
        public static ILogger CreateLogger(NLoggerSection config)
        {
            if (config == null)
            {
                return CreateNullLogger();
            }

            if (config.HasFileLogSection())
            {
                return CreateFileLogger(
                    config.File.Path,
                    config.File.MaxSize,
                    config.File.MaxFiles,
                    config.LogLevel,
                    config.File.IncrementCurrent);
            }
            
            if (config.HasEventLogSection())
            {
                return CreateEventLogLogger(config.EventLog.Source, config.LogLevel);
            }
            
            return CreateNullLogger();
        }

        /// <summary>
        /// Create a file logger instance
        /// </summary>
        /// <param name="file">File to write log entries to</param>
        /// <param name="fileSize">Maximum size of log file in KB</param>
        /// <param name="numFiles">Maximum number of log files to keep</param>
        /// <param name="level">Logging level</param>
        /// <param name="incrementCurrent">Which file to write to</param>
        /// <returns>File logger instance</returns>
        public static ILogger CreateFileLogger(string file, int fileSize, int numFiles, LoggingLevel level, bool incrementCurrent = false)
        {
            var fileWriter = new FileWriter(file, fileSize, numFiles, incrementCurrent);
            return new Logger(fileWriter, level);
        }

        /// <summary>
        /// Create a Windows Event Logger instance
        /// </summary>
        /// <param name="source">Source name to provide to Windows Event Log</param>
        /// <param name="level">Logging level</param>
        /// <returns>Event Logger instance</returns>
        public static ILogger CreateEventLogLogger(string source, LoggingLevel level)
        {
            var eventLogWriter = new EventLogWriter(source);
            return new Logger(eventLogWriter, level);
        }

        /// <summary>
        /// Create a logger instance which ignores all messages
        /// </summary>
        /// <returns>Null Logger instance</returns>
        public static ILogger CreateNullLogger()
        {
            return new Logger(new NullWriter(), 0);
        }
    }
}