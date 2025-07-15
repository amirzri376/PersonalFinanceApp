using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

namespace PersonalFinanceApp.Models
{
    public class TransactionManager
    {
        private readonly List<Transaction> _transactions = new();

        private static readonly JsonSerializerOptions _jsonOptions;

        static TransactionManager()
        {
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                TypeInfoResolver = new DefaultJsonTypeInfoResolver
                {
                    Modifiers =
            {
                typeInfo =>
                {
                    if (typeInfo.Type == typeof(Transaction))
                    {
                        typeInfo.PolymorphismOptions = null; // ⛔ Disable metadata
                    }
                }
            }
                }
            };

            _jsonOptions.Converters.Add(new TransactionJsonConverter());
        }


        public void AddTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            return _transactions;
        }

        public decimal GetTotalExpenses()
        {
            return _transactions
                .Where(t => t.Type == "Expense")
                .Sum(t => t.Amount);
        }

        public decimal GetTotalIncome()
        {
            return _transactions
                .Where(t => t.Type == "Income")
                .Sum(t => t.Amount);
        }

        public decimal GetNetBalance()
        {
            return GetTotalIncome() - GetTotalExpenses();
        }

        public void SaveToFile(string filePath)
        {
            var json = JsonSerializer.Serialize(_transactions, _jsonOptions);
            File.WriteAllText(filePath, json);
        }

        public void LoadFromFile(string filePath)
        {
            if (!File.Exists(filePath)) return;

            var json = File.ReadAllText(filePath);
            var loaded = JsonSerializer.Deserialize<List<Transaction>>(json, _jsonOptions);
            if (loaded != null)
            {
                _transactions.Clear();
                _transactions.AddRange(loaded);
            }
        }
    }
}
