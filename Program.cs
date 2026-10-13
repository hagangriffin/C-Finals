using System;
using System.Collections.Generic;
using System.Linq;

namespace FinalsProject
{
    public class User
    {
        public virtual bool Login()
        {
            String username = "";
            String password = "";

            Console.WriteLine("Enter username: ");
            String inputUsername = Console.ReadLine();
            Console.WriteLine("Enter password: ");
            String inputPassword = Console.ReadLine();

            if (inputUsername == username && inputPassword == password)
            {
                Console.WriteLine("Login successful");
                return true;
            }
            else
            {
                Console.WriteLine("Login failed");
                return false;
            }
        }
    }

    public abstract class Admin : User
    {
        public abstract void AddUser();
        public abstract void RemoveUser();

        // Other methods and properties specific to Admin can be added here

    }

    // ---------- DELEGATES ----------
    public delegate decimal TransactionOperation(decimal balance, decimal amount);
    public delegate void TransactionHandler(string message);

    // ---------- TRANSACTION RECORD ----------
    public class Transaction
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }

    // ---------- BANK ACCOUNT ----------
    public class BankAccount
    {
        private readonly List<Transaction> transactions = new List<Transaction>();
        public decimal Balance { get; private set; }

        // Event built on a delegate: notifies the UI after each transaction
        public event TransactionHandler OnTransaction;

        // Custom delegate + lambda expressions
        private readonly TransactionOperation depositOp = (balance, amount) => balance + amount;
        private readonly TransactionOperation withdrawOp = (balance, amount) => balance - amount;

        // Built-in Func delegates + lambda expressions
        private readonly Func<decimal, bool> isValidAmount = amount => amount > 0;
        private readonly Func<decimal, decimal, bool> hasSufficientFunds = (balance, amount) => balance >= amount;

        public void Deposit(decimal amount)
        {
            if (!isValidAmount(amount))
            {
                Console.WriteLine("Invalid amount. It must be greater than zero.");
                return;
            }
            Record("Deposit", amount, depositOp);
        }

        public void Withdraw(decimal amount)
        {
            if (!isValidAmount(amount))
            {
                Console.WriteLine("Invalid amount. It must be greater than zero.");
                return;
            }
            if (!hasSufficientFunds(Balance, amount))
            {
                Console.WriteLine("Insufficient funds.");
                return;
            }
            Record("Withdrawal", amount, withdrawOp);
        }

        private void Record(string type, decimal amount, TransactionOperation operation)
        {
            Balance = operation(Balance, amount);   // delegate call

            transactions.Add(new Transaction
            {
                Type = type,
                Amount = amount
            });

            OnTransaction?.Invoke($"{type} of {amount:C} successful. New balance: {Balance:C}");
        }

        public void ViewAccountStatement()
        {
            Console.WriteLine("\n===== ACCOUNT STATEMENT =====");

            if (!transactions.Any())
            {
                Console.WriteLine("No transactions yet.");
            }
            else
            {
                // Lambdas inside LINQ
                decimal totalDeposits = transactions.Where(t => t.Type == "Deposit").Sum(t => t.Amount);
                decimal totalWithdrawals = transactions.Where(t => t.Type == "Withdrawal").Sum(t => t.Amount);

                Console.WriteLine("-----------------------------");
                Console.WriteLine($"Total deposits:    {totalDeposits:C}");
                Console.WriteLine($"Total withdrawals: {totalWithdrawals:C}");
            }

            Console.WriteLine($"Current balance:   {Balance:C}");
        }
    }

    public class UserInterface
    {
        private User user = new User();
        private BankAccount account = new BankAccount();

        public UserInterface()
        {
            account.OnTransaction += message => Console.WriteLine(message);
        }

        // FIRST MENU
        public void DisplayMenu()
        {
            int choice = 0;

            while (choice != 4)
            {
                Console.WriteLine("\n1. Login");
                Console.WriteLine("2. Add Asset");
                Console.WriteLine("3. Remove Asset");
                Console.WriteLine("4. Exit");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        if (user.Login())
                        {
                            BankingMenu();   // opens the second menu
                        }
                        break;
                    case 2:
                        // Call method to add asset
                        break;
                    case 3:
                        // Call method to remove asset
                        break;
                    case 4:
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        // SECOND MENU
        private void BankingMenu()
        {
            int choice = 0;

            while (choice != 4)
            {
                Console.WriteLine("\n1. Add Transaction");
                Console.WriteLine("2. Withdrawal");
                Console.WriteLine("3. View Account Statement");
                Console.WriteLine("4. Log Out");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Amount: ");
                        account.Deposit(Convert.ToDecimal(Console.ReadLine()));
                        break;
                    case 2:
                        Console.Write("Amount: ");
                        account.Withdraw(Convert.ToDecimal(Console.ReadLine()));
                        break;
                    case 3:
                        account.ViewAccountStatement();
                        break;
                    case 4:
                        Console.WriteLine("Logged out.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            UserInterface ui = new UserInterface();
            ui.DisplayMenu();
        }
    }
}