using System;
using System.Collections.Generic;
using FinanceManagementSystem.Models;
using FinanceManagementSystem.Processors;
using FinanceManagementSystem.Accounts;

namespace FinanceManagementSystem.App
{
    public class FinanceApp
    {
        private readonly List<Transaction> _transactions;

        public FinanceApp()
        {
            _transactions = new List<Transaction>();
        }

        public void Run()
        {
            var savingsAccount = new SavingsAccount("SAV001", 1000.00m);

            var transaction1 = new Transaction(1, DateTime.Now.AddDays(-2), 150.00m, "Groceries");
            var transaction2 = new Transaction(2, DateTime.Now.AddDays(-1), 200.00m, "Utilities");
            var transaction3 = new Transaction(3, DateTime.Now, 75.50m, "Entertainment");

            var mobileMoneyProcessor = new MobileMoneyProcessor();
            var bankTransferProcessor = new BankTransferProcessor();
            var cryptoWalletProcessor = new CryptoWalletProcessor();

            mobileMoneyProcessor.Process(transaction1);
            bankTransferProcessor.Process(transaction2);
            cryptoWalletProcessor.Process(transaction3);

            savingsAccount.ApplyTransaction(transaction1);
            savingsAccount.ApplyTransaction(transaction2);
            savingsAccount.ApplyTransaction(transaction3);

            _transactions.Add(transaction1);
            _transactions.Add(transaction2);
            _transactions.Add(transaction3);
        }
    }
}
