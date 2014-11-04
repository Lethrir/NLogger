using System;
using System.IO;

namespace NLogger
{
    public class FileWriter : ILogWriter
    {
        private readonly string _file;
        private readonly Random _random;
        private int _currentDelay;
        private readonly bool _incrementCurrent;
        private int _currentFile;

        private readonly int _maxFileSize;
        private readonly int _maxFiles;

        public FileWriter(string file, int fileSize, int numFiles, bool incrementCurrent)
        {
            _maxFileSize = 1024*fileSize;
            _maxFiles = numFiles;
            _file = file;
            _random = new Random();
            _currentDelay = _random.Next(10, 100);
            _incrementCurrent = incrementCurrent;

            if (_incrementCurrent)
            {
                SetInitialCurrentFile();
            }
        }

        public void AppendText(string text, LoggingLevel level)
        {
            var written = false;
            var tries = 0;

            var time = DateTime.Now.ToLongTimeString();
            var date = DateTime.Now.ToShortDateString();

            while (!written && tries < 3)
            {
                try
                {
                    if (GetLength() > _maxFileSize)
                    {
                        if (_incrementCurrent)
                        {
                            _currentFile++;
                            RemoveOldFiles();
                        }
                        else
                        {
                            RollFiles();
                        }
                    }
                    var logText =
                        tries > 0
                            ? string.Format("{0} {1} {2} after {3} tries\r\n", date, time, text, tries)
                            : string.Format("{0} {1} {2}\r\n", date, time, text);
                    
                    File.AppendAllText(FileName(), logText);
                    
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

        private void RemoveOldFiles()
        {
            var files = GetDirectoryFiles();
            var lowerBound = _currentFile - _maxFiles;
            foreach (var file in files)
            {
                var num = GetFileNumber(file);
                if (num <= lowerBound)
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception)
                    {
                        // We don't really want the application to fail because we can't clear the log directory
                    }
                }
            }
        }

        private long GetLength()
        {
           long len = 0;
           if (File.Exists(FileName())) {
              var fi = new FileInfo(FileName());
              len = fi.Length;
           }
           return len;
        }

        private void RollFiles()
        {
            for (int i = _maxFiles; i > 2; i--)
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

        private string FileName()
        {
            return _incrementCurrent ? NumberedFile(_currentFile) : _file;
        }

        private void SetInitialCurrentFile()
        {
            var idx = _file.LastIndexOf('\\');
            if (idx < 0)
            {
                _currentFile = 0;
            }
            else
            {
                var files = GetDirectoryFiles();
                var max = -1;
                foreach (var file in files)
                {
                    var x = GetFileNumber(file);
                    if (x > max)
                    {
                        max = x;
                    }
                }

                _currentFile = ++max;
            }
        }

        private static int GetFileNumber(string file)
        {
            var pidx = file.LastIndexOf('.');
            var num = file.Remove(0, pidx + 1);
            int x;
            int.TryParse(num, out x);
            return x;
        }

        private string[] GetDirectoryFiles()
        {
            var idx = _file.LastIndexOf('\\');
            var folder = _file.Remove(idx, _file.Length - idx);
            var fileName = _file.Remove(0, idx + 1);
            var files = Directory.GetFiles(folder, fileName + "*");
            return files;
        }
    }
}