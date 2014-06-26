using System;
using System.IO;

namespace NLogger
{
    public class FileWriter : ILogWriter
    {
        private readonly string _file;
        private readonly Random _random;
        private int _currentDelay;

        private readonly int _maxFileSize;
        private readonly int _maxFiles;

        public FileWriter(string file)
        {
            _maxFileSize = 1024*4;
            _maxFiles = 3;
            _file = file;
            _random= new Random();
            _currentDelay = _random.Next(10, 100);
        }

        public void AppendText(string text)
        {
            var written = false;
            var tries = 0;

            var time = DateTime.Now.ToLongTimeString();
            var date = DateTime.Now.ToShortDateString();

            while (!written && tries < 3)
            {
                try
                {
                    var fi = new FileInfo(_file);
                    if (fi.Length > _maxFileSize)
                    {
                        RollFiles();
                    }
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

        private void RollFiles()
        {
            for (var i = _maxFiles; i > 2; i--)
            {
                var target = NumberedFile(i);
                var previous = NumberedFile(i - 1);
                File.Delete(target);
                if (File.Exists(previous))
                {
                    File.Move(previous, target);
                }
            }
            if (_maxFiles > 1)
            {
                var target = NumberedFile(2);
                File.Delete(target);
                if (File.Exists(_file))
                {
                    File.Move(_file, target);
                }
            }
            else
            {
                File.Delete(_file);
            }
        }

        private string NumberedFile(int num)
        {
            return string.Format("{0}.{1}", _file, num);
        }
    }
}