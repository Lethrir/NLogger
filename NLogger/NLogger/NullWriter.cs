namespace NLogger
{
    /// <summary>
    /// Implements an ILogWriter which does nothing with messages
    /// </summary>
    public class NullWriter : ILogWriter
    {
        /// <summary>
        /// Implements an ILogWriter which does nothing with messages
        /// </summary>
        public NullWriter() { }

        public void AppendText(string text, LoggingLevel level)
        {
            
        }
    }
}