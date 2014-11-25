using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NLogger.Tests.NoConfig
{
    [TestClass]
    public class LoggerFactoryTests
    {
        [TestMethod]
        public void CreateLoggerNoConfig()
        {
            var logger = LoggerFactory.CreateLogger();
            Assert.IsInstanceOfType(logger, typeof(ILogger));
        }
    }
}
