using System;
using InventoryRecordSystem.App;

namespace InventoryRecordSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "inventory.json";
            var app = new InventoryApp(filePath);

            app.SeedSampleData();
            app.SaveData();

            Console.WriteLine("\n--- New Session ---");
            var newApp = new InventoryApp(filePath);
            newApp.LoadData();
            newApp.PrintSelectedItems();
        }
    }
}
