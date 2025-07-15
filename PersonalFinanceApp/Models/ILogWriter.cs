using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceApp.Models
{
    public interface ILogWriter
    {
        void WriteLine(string message);
    }
}
