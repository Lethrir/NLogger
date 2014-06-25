using System;
using System.IO;

namespace NLogger
{
    public class FileWriter
    {
        private readonly Random _random;
        private int _currentDelay;

        public FileWriter()
        {
            _random= new Random();
            _currentDelay = _random.Next(10, 100);
        }

        public void AppendText(string file, string text)
        {
            var written = false;
            var tries = 0;
            while (!written && tries < 3)
            {
                try
                {
                    if (tries > 0)
                    {
                        File.AppendAllText(file, text + " after " + tries + " tries " + "\r\n");
                    }
                    else
                    {
                        File.AppendAllText(file, text + "\r\n");
                    }
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