using System;
using System.IO;

namespace NLogger
{
    public class FileWriter : ILogWriter
    {
        private readonly string _file;
        private readonly Random _random;
        private int _currentDelay;

        public FileWriter(string file)
        {
            _file = file;
            _random= new Random();
            _currentDelay = _random.Next(10, 100);
        }

        public void AppendText(string text)
        {
            var written = false;
            var tries = 0;
            
            while (!written && tries < 3)
            {
                try
                {
                    var time = DateTime.Now.ToLongTimeString();
                    var date = DateTime.Now.ToShortDateString();
                    var logText =
                        tries > 0
                            ? string.Format("{0} {1} {2} after {3} tries\r\n", date, time, text, tries)
                            : string.Format("{0} {1} {2}\r\n", date, time, text);
                    File.AppendAllText(_file, logText);
                    written = true;
                }
                catch (Exception)
                {
                    tries++;
                    _currentDelay = _random.Next(10, 100);
                    System.Threading.Thread.Sleep(_currentDelay);
                }
            }
        }
    }
}