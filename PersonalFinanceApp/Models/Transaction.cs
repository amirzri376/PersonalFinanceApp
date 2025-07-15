using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceApp.Models
{
    public abstract class Transaction
    {
        public decimal Amount { get; }
        public string Description { get; }

        public DateTime Date { get; }

        public Category? Category { get; }

        protected Transaction(decimal amount, string description, DateTime date, Category? category)
        {
            Amount = amount;
            Description = description;
            Date = date;
            Category = category;
        }

        public abstract string Type { get; }
    }
}
