using System.Diagnostics;

namespace NLogger
{
    /// <summary>
    /// Implements an ILogWriter which writes entries to the Windows Event Log
    /// </summary>
    public class EventLogWriter : ILogWriter
    {
        private readonly string _source;

        /// <summary>
        /// Implements an ILogWriter which writes entries to the Windows Event Log
        /// </summary>
        /// <param name="source">Source name to write to the event log</param>
        public EventLogWriter(string source)
        {
            _source = source;
            if (!EventLog.SourceExists(_source))
            {
                EventLog.CreateEventSource(_source, "Application");
            }
        }

        /// <summary>
        /// Write a log entry
        /// </summary>
        /// <param name="text">Message to log</param>
        /// <param name="level">Level of the message</param>
        public void AppendText(string text, LoggingLevel level)
        {
            EventLog.WriteEntry(_source, text);
        }
    }
}