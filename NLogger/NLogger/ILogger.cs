using System;

namespace NLogger
{
    public interface ILogger
    {
        /// <summary>
        /// Unconditionally write to log
        /// </summary>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Write(string message, params string[] args);

        /// <summary>
        /// Write to log if level is configured for logging
        /// </summary>
        /// <param name="level"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        void Write(LoggingLevel level, string message, params string[] args);

        void LogCritical(string message);
        void LogException(Exception exception);
        void LogError(string message);
        void LogWarning(string message);
        void LogInfo(string message);
        void LogDiagnostic(string message);
    }
}