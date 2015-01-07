namespace NLogger
{
    /// <summary>
    /// Log message severity levels
    /// The logger has a level configured and all messages at or below that level are logged
    /// Diagnostic is the first level to be ignores, Critical is the last
    /// </summary>
    public enum LoggingLevel
    {
        None = 0,
        Critical = 1,
        Exception = 2,
        Error = 3,
        Warning = 4,
        Info = 5,
        Diagnostic = 6
    }
}