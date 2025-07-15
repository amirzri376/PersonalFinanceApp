using System;
using System.Text.Json.Serialization;

namespace PersonalFinanceApp.Models
{
    public class Income : Transaction
    {
        [JsonConstructor]
        public Income(decimal amount, string description, DateTime date, Category? category = null)
            : base(amount, description, date, category)
        {
        }

        public override string Type => "Income";
    }
}
