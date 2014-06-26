using System;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NLogger.Tests
{
    [TestClass]
    public class ConfigurationTests
    {
        private NLoggerSection _config;

        [TestInitialize]
        public void Init()
        {
            _config = (NLoggerSection)ConfigurationManager.GetSection("nLogger");
        }

        [TestMethod]
        public void GetFilePath()
        {
            Assert.AreEqual("C:\\Logs\\Log.log", _config.FilePath);
        }

        [TestMethod]
        public void GetFileSize()
        {
            Assert.AreEqual(10, _config.FileSize);
        }

        [TestMethod]
        public void GetNumFiles()
        {
            Assert.AreEqual(3, _config.NumFiles);
        }

        [TestMethod]
        public void GetLogLevel()
        {
            Assert.AreEqual(LoggingLevel.Error, _config.LogLevel);
        }
    }
}
