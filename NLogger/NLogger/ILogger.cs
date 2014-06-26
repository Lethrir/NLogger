using System;

namespace NLogger
{
    public interface ILogger
    {
        /// <summary>
        /// Write to log if level is configured for logging
        /// </summary>
        /// <param name="level">Level of the message to record</param>
        /// <param name="message">Message to record</param>
        /// <param name="args">Parameters to put in the message template</param>
        void Write(LoggingLevel level, string message, params string[] args);

        void LogCritical(string message);
        void LogException(Exception exception);
        void LogError(string message);
        void LogWarning(string message);
        void LogInfo(string message);
        void LogDiagnostic(string message);
    }
}