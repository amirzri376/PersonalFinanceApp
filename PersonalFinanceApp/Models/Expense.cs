using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceApp.Models
{
    public class Expense : Transaction
    {
        public Expense(decimal amount, string description, DateTime date, Category? category = null) : base(amount, description, date, category) { }

        public override string Type => "Expense";
    }
}
