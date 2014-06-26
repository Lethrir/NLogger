namespace NLogger
{
    public interface ILogWriter
    {
        void AppendText(string text, LoggingLevel level);
    }
}