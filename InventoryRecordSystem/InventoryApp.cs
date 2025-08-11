using System;
using InventoryRecordSystem.Models;
using InventoryRecordSystem.Logger;

namespace InventoryRecordSystem.App
{
    public class InventoryApp
    {
        private readonly InventoryLogger<InventoryItem> _logger;

        public InventoryApp(string filePath)
        {
            _logger = new InventoryLogger<InventoryItem>(filePath);
        }

        public void SeedSampleData()
        {
            _logger.Add(new InventoryItem(1, "Laptop", 10, DateTime.Now));
            _logger.Add(new InventoryItem(2, "Smartphone", 25, DateTime.Now));
            _logger.Add(new InventoryItem(3, "Monitor", 15, DateTime.Now));
        }

        public void SaveData() => _logger.SaveToFile();

        public void LoadData() => _logger.LoadFromFile();

        public void PrintSelectedItems()
        {
            var items = _logger.GetAll();
            foreach (var item in items)
            {
        if (item.Id >= 1 && item.Id <= 3)
                {
                    Console.WriteLine($"\nID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}, Date Added: {item.DateAdded}");
                }
            }
        }
    }
}
