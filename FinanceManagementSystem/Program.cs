using System;
using System.Collections.Generic;
using System.Linq;

namespace FinanceManagementSystem
{
    // Record for financial data
    public record Transaction(int Id, DateTime Date, decimal Amount, string Category);

    // Interface for payment processing
    public interface ITransactionProcessor
    {
        void Process(Transaction transaction);
    }

    // Three concrete processor implementations
    public class BankTransferProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Bank Transfer: Processing {transaction.Amount:C} for {transaction.Category}");
        }
    }

    public class MobileMoneyProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Mobile Money: Processing {transaction.Amount:C} for {transaction.Category}");
        }
    }

    public class CryptoWalletProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Crypto Wallet: Processing {transaction.Amount:C} for {transaction.Category}");
        }
    }

    // Base account class
    public class Account
    {
        public string AccountNumber { get; }
        public decimal Balance { get; protected set; }

        public Account(string accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        public virtual void ApplyTransaction(Transaction transaction)
        {
            Balance -= transaction.Amount;
        }
    }

    // Sealed savings account class
    public sealed class SavingsAccount : Account
    {
        public SavingsAccount(string accountNumber, decimal initialBalance) 
            : base(accountNumber, initialBalance)
        {
        }

        public override void ApplyTransaction(Transaction transaction)
        {
            if (transaction.Amount > Balance)
            {
                Console.WriteLine("Insufficient funds");
            }
            else
            {
                Balance -= transaction.Amount;
                Console.WriteLine($"Updated balance: {Balance:C}");
            }
        }
    }

    // Main application class
    public class FinanceApp
    {
        private List<Transaction> _transactions;

        public FinanceApp()
        {
            _transactions = new List<Transaction>();
        }

        public void Run()
        {
            // i. Instantiate a SavingsAccount with an account number and initial balance (e.g., 1000)
            var savingsAccount = new SavingsAccount("SAV001", 1000.00m);

            // ii. Create three Transaction records with sample values
            var transaction1 = new Transaction(1, DateTime.Now.AddDays(-2), 150.00m, "Groceries");
            var transaction2 = new Transaction(2, DateTime.Now.AddDays(-1), 200.00m, "Utilities");
            var transaction3 = new Transaction(3, DateTime.Now, 75.50m, "Entertainment");

            // iii. Use the following processors to process each transaction
            var mobileMoneyProcessor = new MobileMoneyProcessor();
            var bankTransferProcessor = new BankTransferProcessor();
            var cryptoWalletProcessor = new CryptoWalletProcessor();

            // MobileMoneyProcessor → Transaction 1
            mobileMoneyProcessor.Process(transaction1);

            // BankTransferProcessor → Transaction 2
            bankTransferProcessor.Process(transaction2);

            // CryptoWalletProcessor → Transaction 3
            cryptoWalletProcessor.Process(transaction3);

            // iv. Apply each transaction to the SavingsAccount using ApplyTransaction
            savingsAccount.ApplyTransaction(transaction1);
            savingsAccount.ApplyTransaction(transaction2);
            savingsAccount.ApplyTransaction(transaction3);

            // v. Add all transactions to _transactions
            _transactions.Add(transaction1);
            _transactions.Add(transaction2);
            _transactions.Add(transaction3);
        }
    }

    // Main program entry point
    class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of FinanceApp and call Run()
            var financeApp = new FinanceApp();
            financeApp.Run();
        }
    }
}
