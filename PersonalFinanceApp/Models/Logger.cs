using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceApp.Models
{
    public class Logger
    {

        private readonly IClock _clock;
        private readonly ILogWriter _writer;

        public Logger(IClock clock, ILogWriter writer)
        {
            _clock = clock;
            _writer = writer;
        }

        public void Log(string message)
        {
            string timestamp = _clock.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _writer.WriteLine($"[{timestamp}] {message}");
        }
    }
}
