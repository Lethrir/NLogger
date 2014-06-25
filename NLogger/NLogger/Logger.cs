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

        public void Write(string message, params string[] args)
        {
            _file.AppendText(_fileName, string.Format(message, args));
        }

        private void LogException(Exception exception, bool isInner)
        {
            if (isInner)
            {
                Write("Inner Exception: {0}, message {1}", exception.GetType().ToString(), exception.Message);
            }
            else
            {
                Write("Exception: {0}, message {1}", exception.GetType().ToString(), exception.Message);
            }
            if (exception.InnerException != null)
            {
                LogException(exception.InnerException, true);
            }
        }

        public void LogException(Exception exception)
        {
            LogException(exception, false);
        }

        public void LogError(string message)
        {
            Write("Error: {0}", message);
        }

        public void LogWarning(string message)
        {
            Write("Warning: {0}", message);
        }

        public void LogInfo(string message)
        {
            Write("Info: {0}", message);
        }

        public void LogDiagnostic(string message)
        {
            Write("Diagnostic: {0}", message);
        }
    }
}