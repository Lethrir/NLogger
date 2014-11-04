namespace NLogger
{
    public class NullWriter : ILogWriter
    {
        public void AppendText(string text, LoggingLevel level)
        {
            
        }
    }
}