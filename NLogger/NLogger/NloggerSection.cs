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

        [ConfigurationProperty("failIfUnauthorized", IsRequired = false, DefaultValue = true)]
        public bool FailIfUnauthorized
        {
            get { return (bool)this["failIfUnauthorized"]; }
            set { this["failIfUnauthorized"] = value; }
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
        [ConfigurationProperty("source", IsRequired = false)]
        public string Source
        {
            get { return (string)this["source"]; }
            set { this["source"] = value; }
        }
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

        /// <summary>
        /// If false, the current file is always path.log
        /// If true, the current file is path.log.x where x increments with each new file - no files will be renamed
        /// </summary>
        [ConfigurationProperty("incrementCurrent", IsRequired = false, DefaultValue = false)]
        public bool IncrementCurrent
        {
            get { return (bool) this["incrementCurrent"]; }
            set { this["incrementCurrent"] = value; }
        }
    }
}