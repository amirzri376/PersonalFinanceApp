using System;
using System.Windows;
using System.Windows.Controls;
using PersonalFinanceApp.Models; // Assuming your core logic is in this namespace

namespace PersonalFinanceApp.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TransactionManager _transactionManager = new TransactionManager();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!decimal.TryParse(AmountTextBox.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid positive amount.");
                return;
            }

            string description = DescriptionTextBox.Text;
            DateTime? date = DatePicker.SelectedDate;
            string categoryText = CategoryTextBox.Text;
            string type = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrWhiteSpace(description) || date == null || string.IsNullOrWhiteSpace(type))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            Category? category = string.IsNullOrWhiteSpace(categoryText)
                 ? null
                 : new Category(categoryText);

            Transaction transaction = type == "Income"
                ? new Income(amount, description, date.Value, category)
                : new Expense(amount, description, date.Value, category);

            _transactionManager.AddTransaction(transaction);

            TransactionGrid.ItemsSource = null;
            TransactionGrid.ItemsSource = _transactionManager.GetTransactions();

            // ✅ Update summary text blocks
            TotalIncomeText.Text = $"{_transactionManager.GetTotalIncome():C}";
            TotalExpensesText.Text = $"{_transactionManager.GetTotalExpenses():C}";
            NetBalanceText.Text = $"{_transactionManager.GetNetBalance():C}";

            // Clear inputs
            AmountTextBox.Clear();
            DescriptionTextBox.Clear();
            CategoryTextBox.Clear();
            DatePicker.SelectedDate = null;
            TypeComboBox.SelectedIndex = -1;

            MessageBox.Show($"{type} added!");
        }

    }
}
