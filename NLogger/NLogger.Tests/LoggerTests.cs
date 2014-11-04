using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NLogger.Tests
{
    [TestClass]
    public class LoggerTests
    {
        private ILogger _logger;

        [TestInitialize]
        public void Init()
        {
            _logger = LoggerFactory.CreateFileLogger("C:\\Logs\\Log.log", 10, 3, LoggingLevel.Diagnostic);
        }

        [TestMethod]
        public void Ctor()
        {
            Assert.IsInstanceOfType(_logger, typeof(Logger));
        }

        [TestMethod]
        public void Write()
        {
            _logger.Write(LoggingLevel.Diagnostic,  "Test");
        }

        [TestMethod]
        public void WriteMultiThreaded()
        {
            var ids = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            Parallel.ForEach(ids, id => _logger.Write(LoggingLevel.Error,  "Item " + id));
        }

        [TestMethod]
        public void LogException()
        {
            _logger.LogException(new Exception("TEST"));
        }

        [TestMethod]
        public void LogExceptionWithInner()
        {
            var inner = new Exception("inner");
            var arg = new ArgumentOutOfRangeException("Argument Exception", inner);
            var outer = new FileNotFoundException("Outer", arg);
            _logger.LogException(outer);
        }

        [TestMethod]
        public void LogError()
        {
            _logger.LogError("Test");
        }

        [TestMethod]
        public void LogWarning()
        {
            _logger.LogWarning("Test");
        }

        [TestMethod]
        public void LogInfo()
        {
            _logger.LogInfo("Test");
        }

        [TestMethod]
        public void LogDiagnostic()
        {
            _logger.LogDiagnostic("Test");
        }

        [TestMethod]
        public void RollFile()
        {
            for (var i = 0; i < 10000; i++)
            {
                _logger.LogInfo(string.Format("Logging {0}", i));
            }
        }

        [TestMethod]
        public void IncrementalLogger()
        {
            var logger = LoggerFactory.CreateFileLogger("C:\\Logs\\Log.log", 10, 3, LoggingLevel.Diagnostic, true);

            for (var i = 0; i < 10000; i++)
            {
                logger.LogInfo(string.Format("Logging {0}", i));
            }
        }
    }
}
