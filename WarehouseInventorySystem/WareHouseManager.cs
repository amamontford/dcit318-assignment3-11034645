using System;
using WarehouseInventorySystem.Models;
using WarehouseInventorySystem.Exceptions;
using WarehouseInventorySystem.Interfaces;

namespace WarehouseInventorySystem
{
    public class WareHouseManager
    {
        private InventoryRepository<ElectronicItem> _electronics = new();
        private InventoryRepository<GroceryItem> _groceries = new();

        public void SeedData()
        {
            _electronics.AddItem(new ElectronicItem(1, "Laptop", 10, "Dell", 24));
            _electronics.AddItem(new ElectronicItem(2, "Smartphone", 15, "Samsung", 12));
            _electronics.AddItem(new ElectronicItem(3, "Tablet", 8, "Apple", 18));

            _groceries.AddItem(new GroceryItem(101, "Rice", 50, DateTime.Now.AddMonths(6)));
            _groceries.AddItem(new GroceryItem(102, "Milk", 30, DateTime.Now.AddDays(7)));
            _groceries.AddItem(new GroceryItem(103, "Bread", 25, DateTime.Now.AddDays(3)));
        }

        public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
        {
            foreach (var item in repo.GetAllItems())
            {
                Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}");
            }
        }

        public InventoryRepository<ElectronicItem> Electronics => _electronics;
        public InventoryRepository<GroceryItem> Groceries => _groceries;
    }
}
