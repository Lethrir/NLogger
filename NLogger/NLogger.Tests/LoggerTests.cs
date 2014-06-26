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
        private Logger _logger;

        [TestInitialize]
        public void Init()
        {
            _logger = LoggerFactory.CreateFileLogger();
        }

        [TestMethod]
        public void Ctor()
        {
            Assert.IsInstanceOfType(_logger, typeof(Logger));
        }

        [TestMethod]
        public void Write()
        {
            _logger.Write("Test");
        }

        [TestMethod]
        public void WriteMultiThreaded()
        {
            var ids = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            Parallel.ForEach(ids, id => _logger.Write("Item " + id));
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
    }
}
