using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceApp.Models
{
    public class TransactionManager
    {
        private readonly List<Transaction> _transactions = new();
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
    }
}
