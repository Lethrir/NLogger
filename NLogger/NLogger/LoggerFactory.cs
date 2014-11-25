using System.Configuration;

namespace NLogger
{
    public static class LoggerFactory
    {
        /// <summary>
        /// Create a logger using app.config or web.config settings
        /// </summary>
        /// <returns></returns>
        public static ILogger CreateLogger()
        {
            var config = (NLoggerSection)ConfigurationManager.GetSection("nLogger");
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

        public static ILogger CreateFileLogger(string file, int fileSize, int numFiles, LoggingLevel level, bool incrementCurrent = false)
        {
            var fileWriter = new FileWriter(file, fileSize, numFiles, incrementCurrent);
            return new Logger(fileWriter, level);
        }

        public static ILogger CreateEventLogLogger(string source, LoggingLevel level)
        {
            var eventLogWriter = new EventLogWriter(source);
            return new Logger(eventLogWriter, level);
        }

        public static ILogger CreateNullLogger()
        {
            return new Logger(new NullWriter(), 0);
        }
    }
}
