using System.Configuration;

namespace NLogger
{
    public class NLoggerSection : ConfigurationSection
    {

        [ConfigurationProperty("logLevel", IsRequired = false, DefaultValue = LoggingLevel.Diagnostic)]
        public LoggingLevel LogLevel
        {
            get { return (LoggingLevel) this["logLevel"]; }
            set { this["logLevel"] = value; }
        }

        [ConfigurationProperty("file")]
        public FileElement File
        {
            get { return (FileElement) this["file"]; }
            set { this["file"] = value; }
        }

        [ConfigurationProperty("eventLog", IsRequired = false, DefaultValue = null)]
        public EventLogElement EventLog
        {
            get { return (EventLogElement)this["eventLog"]; }
            set { this["eventLog"] = value; }   
        }

        public bool HasEventLogSection()
        {
            return ((EventLogElement)this["eventLog"]).ElementInformation.IsPresent;
        }

        public bool HasFileLogSection()
        {
            return ((FileElement)this["file"]).ElementInformation.IsPresent;
        }
    }

    public class EventLogElement : ConfigurationElement
    {
        
    }

    public class FileElement : ConfigurationElement
    {
        [ConfigurationProperty("path", IsRequired = false)]
        public string Path
        {
            get { return (string)this["path"]; }
            set { this["path"] = value; }
        }

        [ConfigurationProperty("maxFiles", IsRequired = false, DefaultValue = 10)]
        public int MaxFiles
        {
            get { return (int)this["maxFiles"]; }
            set { this["maxFiles"] = value; }
        }

        [ConfigurationProperty("maxSize", IsRequired = false, DefaultValue = 1000)]
        public int MaxSize
        {
            get { return (int)this["maxSize"]; }
            set { this["maxSize"] = value; }
        }
    }
}