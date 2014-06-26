using System.Configuration;

namespace NLogger
{
    public static class LoggerFactory
    {
        public static Logger CreateFileLogger()
        {
            var file = ConfigurationManager.AppSettings["LogFile"];
            var fileWriter = new FileWriter(file);
            return new Logger(fileWriter, LoggingLevel.Diagnostic);
        }
    }
}
