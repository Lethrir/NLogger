using System;

namespace NLogger
{
    public class ConsoleWriter : ILogWriter
    {
        public void AppendText(string text, LoggingLevel level)
        {
            Console.WriteLine(text);
        }
    }
}