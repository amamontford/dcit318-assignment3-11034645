using System;
using WarehouseInventorySystem.Models;

namespace WarehouseInventorySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new WareHouseManager();
            manager.SeedData();

            Console.WriteLine("=== Grocery Items ===");
            manager.PrintAllItems(manager.Groceries);

            Console.WriteLine("\n=== Electronic Items ===");
            manager.PrintAllItems(manager.Electronics);
        }
    }
}
