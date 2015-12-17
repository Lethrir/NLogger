using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NLogger.Tests
{
    [TestClass]
    public class FileWriterTests
    {
        [TestMethod]
        public void Test()
        {
            var fw = new FileWriter("C:\\testfolder\\test\\test\\logfile.log", 1000, 3, false, true);
            fw.AppendText("Test", LoggingLevel.Error);
        }

        [TestMethod]
        public void Test2()
        {
            var fw = new FileWriter("C:\\testfolder\\test\\test\\logfile.log", 1000, 3, true, true);
            fw.AppendText("Test", LoggingLevel.Error);
        }
    }
}