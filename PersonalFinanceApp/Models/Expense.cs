using System;
using System.Text.Json.Serialization;

namespace PersonalFinanceApp.Models
{
    public class Expense : Transaction
    {
        [JsonConstructor]
        public Expense(decimal amount, string description, DateTime date, Category? category = null)
            : base(amount, description, date, category)
        {
        }

        public override string Type => "Expense";
    }
}
