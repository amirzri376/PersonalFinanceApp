using System;

namespace PersonalFinanceApp.Models
{
    public class SystemClock : IClock
    {
        public DateTime Now => DateTime.Now;
    }
}