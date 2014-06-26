using System.Configuration;

namespace NLogger
{
    public class NLoggerSection : ConfigurationSection
    {
        [ConfigurationProperty("filePath", IsRequired = false)]
        public string FilePath
        {
            get { return (string) this["filePath"]; }
            set { this["filePath"] = value; }
        }

        [ConfigurationProperty("numFiles", IsRequired = false, DefaultValue = 10)]
        public int NumFiles
        {
            get { return (int)this["numFiles"]; }
            set { this["numFiles"] = value; }
        }

        [ConfigurationProperty("fileSize", IsRequired = false, DefaultValue = 1000)]
        public int FileSize
        {
            get { return (int)this["fileSize"]; }
            set { this["fileSize"] = value; }
        }

        [ConfigurationProperty("logLevel", IsRequired = false, DefaultValue = LoggingLevel.Diagnostic)]
        public LoggingLevel LogLevel
        {
            get { return (LoggingLevel) this["logLevel"]; }
            set { this["logLevel"] = value; }
        }

    }
}