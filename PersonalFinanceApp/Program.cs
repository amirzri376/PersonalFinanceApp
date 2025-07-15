using System;
using System.IO;
using System.Linq;
using PersonalFinanceApp.Models;

TransactionManager manager = new();
string dataFile = Path.GetFullPath(
    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\Data\transactions.json")
);
Console.WriteLine($"Looking for file at: {dataFile}");

// Load existing transactions if file exists
if (File.Exists(dataFile))
{
    manager.LoadFromFile(dataFile);
}

var clock = new SystemClock();
var writer = new FileLogWriter("log.txt");
var logger = new Logger(clock, writer);

logger.Log("Application started.");

while (true)
{
    Console.WriteLine("\n--- Personal Finance Manager ---");
    Console.WriteLine("1. Add Income");
    Console.WriteLine("2. Add Expense");
    Console.WriteLine("3. View All Transactions");
    Console.WriteLine("4. Show Balance Summary");
    Console.WriteLine("0. Exit");
    Console.Write("Select an option: ");

    string? input = Console.ReadLine();
    Console.WriteLine();

    switch (input)
    {
        case "1":
            AddTransaction(manager, isIncome: true);
            break;
        case "2":
            AddTransaction(manager, isIncome: false);
            break;
        case "3":
            ViewTransactions(manager);
            break;
        case "4":
            ShowBalanceSummary(manager);
            break;
        case "0":
            manager.SaveToFile(dataFile);
            logger.Log("Application exited.");
            return;
        default:
            Console.WriteLine("Invalid option. Try again.");
            break;
    }
}

static void AddTransaction(TransactionManager manager, bool isIncome)
{
    Console.Write("Enter amount: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
    {
        Console.WriteLine("Invalid amount.");
        return;
    }

    Console.Write("Enter description: ");
    string description = Console.ReadLine();

    Console.Write("Enter date (yyyy-MM-dd): ");
    string? dateInput = Console.ReadLine();

    if (!DateTime.TryParseExact(dateInput, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime date))
    {
        Console.WriteLine("Invalid date format. Please use yyyy-MM-dd.");
        return;
    }

    Console.Write("Enter category (optional): ");
    string? categoryInput = Console.ReadLine();
    Category? category = string.IsNullOrWhiteSpace(categoryInput)
        ? null
        : new Category(categoryInput);

    Transaction transaction = isIncome
        ? new Income(amount, description, date, category)
        : new Expense(amount, description, date, category);

    manager.AddTransaction(transaction);
    Console.WriteLine($"{transaction.Type} added successfully.\n");
}

static void ViewTransactions(TransactionManager manager)
{
    var transactions = manager.GetTransactions().ToList();

    if (!transactions.Any())
    {
        Console.WriteLine("No transactions found.");
        return;
    }

    Console.WriteLine("\n--- All Transactions ---");
    Console.WriteLine("{0,-10} | {1,-10} | {2,-15} | {3}", "Type", "Amount", "Category", "Date");
    Console.WriteLine(new string('-', 55));

    foreach (var t in transactions)
    {
        string categoryDisplay = t.Category?.ToString() ?? "None";
        Console.WriteLine("{0,-10} | {1,-10:C} | {2,-15} | {3:yyyy-MM-dd}",
            t.Type, t.Amount, categoryDisplay, t.Date);
    }
}

static void ShowBalanceSummary(TransactionManager manager)
{
    decimal totalIncome = manager.GetTotalIncome();
    decimal totalExpenses = manager.GetTotalExpenses();
    decimal balance = totalIncome - totalExpenses;

    Console.WriteLine("\n--- Balance Summary ---");
    Console.WriteLine($"Total Income   : {totalIncome:C}");
    Console.WriteLine($"Total Expenses : {totalExpenses:C}");
    Console.WriteLine($"Net Balance    : {balance:C}");
}
