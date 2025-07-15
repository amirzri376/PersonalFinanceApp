using Xunit;
using PersonalFinanceApp.Models;
using System;

namespace PersonalFinanceApp.Tests
{
    public class TransactionManagerTests
    {
        [Fact]
        public void GetNetBalance_ReturnsCorrectDifference()
        {
            // Arrange
            var manager = new TransactionManager();

            // Add income: 1000
            var income = new Income(1000m, "Salary", new DateTime(2024, 1, 1), null);
            manager.AddTransaction(income);

            // Add expense: 300
            var expense = new Expense(300m, "Groceries", new DateTime(2024, 1, 2), null);
            manager.AddTransaction(expense);

            // Act
            decimal netBalance = manager.GetNetBalance();

            // Assert
            Assert.Equal(700m, netBalance);
        }

        [Fact]
        public void GetTotalIncome_ReturnsSumOfIncomes()
        {
            // Arrange
            var manager = new TransactionManager();
            manager.AddTransaction(new Income(1000m, "Salary", DateTime.Today, null));
            manager.AddTransaction(new Income(500m, "Freelance", DateTime.Today, null));

            // Act
            decimal totalIncome = manager.GetTotalIncome();

            // Assert
            Assert.Equal(1500m, totalIncome);
        }

        [Fact]
        public void GetTotalExpenses_ReturnsSumOfExpenses()
        {
            // Arrange
            var manager = new TransactionManager();
            manager.AddTransaction(new Expense(300m, "Groceries", DateTime.Today, null));
            manager.AddTransaction(new Expense(200m, "Transport", DateTime.Today, null));

            // Act
            decimal totalExpenses = manager.GetTotalExpenses();

            // Assert
            Assert.Equal(500m, totalExpenses);
        }

        [Fact]
        public void GetTotalExpenses_ReturnsCorrectSumOfExpenses()
        {
            var manager = new TransactionManager();
            manager.AddTransaction(new Expense(50m, "Groceries", new DateTime(2024, 1, 1)));
            manager.AddTransaction(new Expense(30m, "Transport", new DateTime(2024, 1, 2)));
            manager.AddTransaction(new Income(100m, "Salary", new DateTime(2024, 1, 3))); // should be ignored

            // Act
            decimal totalExpenses = manager.GetTotalExpenses();

            // Assert
            Assert.Equal(80m, totalExpenses);
        }
    }
}
