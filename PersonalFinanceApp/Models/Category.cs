using System;
using System.Text.Json.Serialization;

namespace PersonalFinanceApp.Models
{
    public class Category
    {
        public string Name { get; }

        [JsonConstructor]
        public Category(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
