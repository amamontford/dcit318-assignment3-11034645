using System;
using FinanceManagementSystem.Interfaces;
using FinanceManagementSystem.Models;

namespace FinanceManagementSystem.Processors
{
    public class BankTransferProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Bank Transfer: Processing {transaction.Amount:C} for {transaction.Category}");
        }
    }
}
