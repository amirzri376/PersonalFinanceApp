using System;
using System.IO;

namespace PersonalFinanceApp.Models
{
    public class FileLogWriter : ILogWriter
    {
        private readonly string _filePath;

        public FileLogWriter(string filePath)
        {
            _filePath = filePath;
        }

        public void WriteLine(string message)
        {
            File.AppendAllText(_filePath, message + Environment.NewLine);
        }
    }
}
