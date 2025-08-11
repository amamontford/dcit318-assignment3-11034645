using System;
using FinanceManagementSystem.Interfaces;
using FinanceManagementSystem.Models;

namespace FinanceManagementSystem.Processors
{
    public class MobileMoneyProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"Mobile Money: Processing {transaction.Amount:C} for {transaction.Category}");
        }
    }
}
