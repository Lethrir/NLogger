using System.Diagnostics;

namespace NLogger
{
    public class EventLogWriter : ILogWriter
    {
        private readonly string _source;

        public EventLogWriter(string source)
        {
            _source = source;
            if (!EventLog.SourceExists(_source))
            {
                EventLog.CreateEventSource(_source, "Application");
            }
        }

        public void AppendText(string text, LoggingLevel level)
        {
            EventLog.WriteEntry(_source, text);
        }
    }
}