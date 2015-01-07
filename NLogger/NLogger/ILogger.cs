using System;

namespace NLogger
{
    public interface ILogger
    {
        /// <summary>
        /// Write to log if level is configured for logging
        /// </summary>
        /// <param name="level">Level of the message to record</param>
        /// <param name="message">Template of the message to record</param>
        /// <param name="args">Parameters to put in the message template</param>
        void Write(LoggingLevel level, string message, params string[] args);

        /// <summary>
        /// Log a critical message
        /// </summary>
        /// <param name="message">Message to log</param>
        void LogCritical(string message);
        
        /// <summary>
        /// Log details of the given exception
        /// </summary>
        /// <param name="exception">Exception to log</param>
        void LogException(Exception exception);

        /// <summary>
        /// Log an error message
        /// </summary>
        /// <param name="message">Message to log</param>
        void LogError(string message);
        
        /// <summary>
        /// Log a warning message
        /// </summary>
        /// <param name="message">Message to log</param>
        void LogWarning(string message);
        
        /// <summary>
        /// Log a message with operational information
        /// </summary>
        /// <param name="message">Message to log</param>
        void LogInfo(string message);
        
        /// <summary>
        /// Log a diagnostic message
        /// This is the highest level of logging and the first to be disabled
        /// </summary>
        /// <param name="message">Message to log</param>
        void LogDiagnostic(string message);
    }
}