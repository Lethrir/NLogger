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
            if (config.HasFileLogSection())
            {
                return CreateFileLogger(
                    config.File.Path,
                    config.File.MaxSize,
                    config.File.MaxFiles,
                    config.LogLevel);
            }
            else if (config.HasEventLogSection())
            {
                return CreateEventLogLogger(config.EventLog.Source, config.LogLevel);
            }
            else
            {
                throw new ConfigurationException("NLogger has no config information for output. Please specify either a file or eventLog config section.");
            }
        }

        public static ILogger CreateFileLogger(string file, int fileSize, int numFiles, LoggingLevel level)
        {
            var fileWriter = new FileWriter(file, fileSize, numFiles);
            return new Logger(fileWriter, level);
        }

        public static ILogger CreateEventLogLogger(string source, LoggingLevel level)
        {
            var eventLogWriter = new EventLogWriter(source);
            return new Logger(eventLogWriter, level);
        }
    }
}
