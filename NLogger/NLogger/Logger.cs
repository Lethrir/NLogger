using System;

namespace NLogger
{
    public class Logger : ILogger
    {
        private readonly LoggingLevel _level;
        private readonly ILogWriter _writer;

        public Logger(ILogWriter writer, LoggingLevel level)
        {
            _writer = writer;
            _level = level;
        }

        /// <summary>
        /// Unconditionally write to log
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Write(string message, params string[] args)
        {
            _writer.AppendText(string.Format(message, args));
        }

        /// <summary>
        /// Write to log if level is configured for logging
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public void Write(LoggingLevel level, string message, params string[] args)
        {
            if ((int) level <= (int) _level)
            {
                Write(message, args);
            }
        }

        public void LogCritical(string message)
        {
            Write(LoggingLevel.Critical, "Critical: {0}", message);
        }

        private void LogException(Exception exception, bool isInner)
        {
            if (isInner)
            {
                Write(LoggingLevel.Exception, "Inner Exception: {0}, message {1}", exception.GetType().ToString(), exception.Message);
            }
            else
            {
                Write(LoggingLevel.Exception, "Exception: {0}, message {1}", exception.GetType().ToString(), exception.Message);
            }
            if (exception.InnerException != null)
            {
                LogException(exception.InnerException, true);
            }
        }

        public void LogException(Exception exception)
        {
            LogException(exception, false);
        }

        public void LogError(string message)
        {
            Write(LoggingLevel.Error, "Error: {0}", message);
        }

        public void LogWarning(string message)
        {
            Write(LoggingLevel.Warning, "Warning: {0}", message);
        }

        public void LogInfo(string message)
        {
            Write(LoggingLevel.Info, "Info: {0}", message);
        }

        public void LogDiagnostic(string message)
        {
            Write(LoggingLevel.Diagnostic, "Diagnostic: {0}", message);
        }
    }
}