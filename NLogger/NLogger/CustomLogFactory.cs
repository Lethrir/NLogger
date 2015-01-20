using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace NLogger
{
    public class CustomLogFactory<T> where T : NLoggerSection
    {
        private readonly List<ILogWriterRegistration<T>> _logWriterTypes;

        public CustomLogFactory()
        {
            _logWriterTypes= new List<ILogWriterRegistration<T>>();
        } 

        public void RegisterLogWriterType(ILogWriterRegistration<T> registration)
        {
            _logWriterTypes.Add(registration);
        }

        /// <summary>
        /// Create a logger using App.config or Web.config settings
        /// </summary>
        /// <returns></returns>
        public ILogger CreateLogger(string sectionName)
        {
            var config = (T) ConfigurationManager.GetSection(sectionName);
            
            if (config != null)
            {
                var writer = GetLogWriter(config);

                if (writer != null)
                {
                    return new Logger(writer, config.LogLevel);
                }
            }

            return LoggerFactory.CreateLogger();
        }

        private ILogWriter GetLogWriter(T config)
        {
            var type = _logWriterTypes.FirstOrDefault(t => t.HasSection(config));

            return type != null
                ? type.GetWriter(config)
                : null;
        }
    }
}
