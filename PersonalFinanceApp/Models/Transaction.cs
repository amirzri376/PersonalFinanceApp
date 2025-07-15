using System;
using System.Text.Json.Serialization;

namespace PersonalFinanceApp.Models
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(Income), "Income")]
    [JsonDerivedType(typeof(Expense), "Expense")]
    public abstract class Transaction
    {
        public decimal Amount { get; }
        public string Description { get; }
        public DateTime Date { get; }
        public Category? Category { get; }

        [JsonConstructor]
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
