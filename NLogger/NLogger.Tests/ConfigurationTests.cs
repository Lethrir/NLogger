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
            Assert.AreEqual("C:\\Logs\\Log.log", _config.File.Path);
        }

        [TestMethod]
        public void GetFileSize()
        {
            Assert.AreEqual(10, _config.File.MaxSize);
        }

        [TestMethod]
        public void GetNumFiles()
        {
            Assert.AreEqual(3, _config.File.MaxFiles);
        }

        [TestMethod]
        public void GetLogLevel()
        {
            Assert.AreEqual(LoggingLevel.Error, _config.LogLevel);
        }

        [TestMethod]
        public void EventLogIsNotPresent()
        {
            Assert.IsFalse(_config.HasEventLogSection());
        }

        [TestMethod]
        public void FileIsPresent()
        {
            Assert.IsTrue(_config.HasFileLogSection());
        }
    }
}