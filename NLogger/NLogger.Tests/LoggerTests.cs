﻿using System;
using System.Collections.Generic;
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
            _logger = new Logger("C:\\Logs\\Test.log");
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
    }
}
