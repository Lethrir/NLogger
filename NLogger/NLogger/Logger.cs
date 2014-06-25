using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLogger
{
    public class Logger
    {
        private readonly string _fileName;
        private readonly FileWriter _file;

        public Logger(string file)
        {
            _file = new FileWriter();
            _fileName = file;
        }

        public void Write(string message)
        {
            _file.AppendText(_fileName, message);
        }
    }
}