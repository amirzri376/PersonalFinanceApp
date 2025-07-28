# PersonalFinanceApp

A simple **personal finance management application** implemented in C# with both **Console** and **WPF GUI** interfaces. Users can track incomes and expenses, view summaries, and persist data across sessions via a shared JSON file.

## Features

✅ Add income and expenses  
✅ View transaction history  
✅ See balance summary (total income, total expenses, net balance)  
✅ Data saved to and loaded from a shared `transactions.json` file  
✅ Two interfaces:
- **Console App** (for quick CLI usage)
- **WPF App** (for desktop GUI interaction)

## Technologies

- C# (.NET 8)
- WPF for desktop UI
- JSON serialization (`System.Text.Json`)
- MVVM-inspired structure
- Git + GitHub for version control

## Project Structure

```
PersonalFinanceApp/
├── PersonalFinanceApp.Models/     # Core logic (Transaction, Manager, Logger)
├── PersonalFinanceApp.ConsoleApp/ # Console interface
├── PersonalFinanceApp.Wpf/        # WPF desktop app
├── Data/
│   └── transactions.json          # Shared data file
├── README.md
```

## Getting Started

### Prerequisites

- .NET 8 SDK
- Visual Studio 2022+ with WPF support

### Run the Console App

1. Open the solution in Visual Studio.
2. Set `PersonalFinanceApp.ConsoleApp` as the startup project.
3. Ensure `Data/transactions.json` exists and is accessible.
4. Run the project. Use the menu to add/view transactions.

### Run the WPF App

1. Set `PersonalFinanceApp.Wpf` as the startup project.
2. Run the project. Use the GUI to enter transactions and view summaries.

> Both apps read and write to the same `Data/transactions.json` file.

## JSON File Setup

Ensure the following:

- `transactions.json` is inside a `Data/` folder at the root of the solution.
- In Visual Studio, right-click the file → Properties:
  - **Build Action**: `Content`
  - **Copy to Output Directory**: `Copy if newer`

## Logging

- All user actions in the Console app are logged to `log.txt`.

## Unit Testing
This project includes unit tests, covering:

Tests verify the correctness of core financial calculations:

GetNetBalance_ReturnsCorrectDifference: Confirms net balance = income − expenses.

GetTotalIncome_ReturnsSumOfIncomes: Validates that all income transactions are summed correctly.

GetTotalExpenses_ReturnsSumOfExpenses: Checks that only expenses are summed, even in presence of mixed transactions.

The Logger class was refactored to depend on IClock and ILoggerWriter interfaces, making it testable. A separate test validates that:

Log messages are correctly formatted.

Each message includes an accurate timestamp.


## Future Improvements

- Export to CSV or PDF
- Monthly/Category summaries
- Budget planning features
- Unit tests and error handling polish

## License

This project is for educational and demo purposes. No license applied.
