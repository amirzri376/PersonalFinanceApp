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
            // 1. Parse amount
            if (!decimal.TryParse(AmountTextBox.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid positive amount.");
                return;
            }

            // 2. Read other inputs
            string description = DescriptionTextBox.Text;
            DateTime? date = DatePicker.SelectedDate;
            string categoryText = CategoryTextBox.Text;
            string type = (TypeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            // 3. Basic validation
            if (string.IsNullOrWhiteSpace(description) || date == null || string.IsNullOrWhiteSpace(type))
            {
                MessageBox.Show("Please fill in all required fields.");
                return;
            }

            // 4. Create category (optional)
            Category? category = string.IsNullOrWhiteSpace(categoryText)
                 ? null
                 : new Category(categoryText);  // uses your constructor

            // 5. Create transaction
            Transaction transaction = type == "Income"
                ? new Income(amount, description, date.Value, category)
                : new Expense(amount, description, date.Value, category);

            // 6. Add to manager
            _transactionManager.AddTransaction(transaction);
            TransactionGrid.ItemsSource = null;
            TransactionGrid.ItemsSource = _transactionManager.GetTransactions();
            BalanceTextBlock.Text = $"Balance: {_transactionManager.GetNetBalance():C}";

            // 7. Clear inputs (optional)
            AmountTextBox.Clear();
            DescriptionTextBox.Clear();
            CategoryTextBox.Clear();
            DatePicker.SelectedDate = null;
            TypeComboBox.SelectedIndex = -1;

            // 8. Confirm
            MessageBox.Show($"{type} added!");
        }
    }
}
