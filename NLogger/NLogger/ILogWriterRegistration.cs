namespace NLogger
{
    public interface ILogWriterRegistration<T> where T : NLoggerSection
    {
        bool HasSection(T config);
        ILogWriter GetWriter(T config);
    }
}
