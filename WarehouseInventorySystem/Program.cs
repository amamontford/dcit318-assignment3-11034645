using System;
using System.Collections.Generic;
using System.Linq;

namespace WarehouseInventorySystem
{
    // a. Marker Interface for Inventory Items
    public interface IInventoryItem
    {
        int Id { get; }
        string Name { get; }
        int Quantity { get; set; }
    }

    // e. Custom Exceptions
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException(string message) : base(message) { }
    }

    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(string message) : base(message) { }
    }

    public class InvalidQuantityException : Exception
    {
        public InvalidQuantityException(string message) : base(message) { }
    }

    // b. ElectronicItem Class
    public class ElectronicItem : IInventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string Brand { get; set; }
        public int WarrantyMonths { get; set; }

        public ElectronicItem(int id, string name, int quantity, string brand, int warrantyMonths)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Brand = brand;
            WarrantyMonths = warrantyMonths;
        }
    }

    // c. GroceryItem Class
    public class GroceryItem : IInventoryItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }

        public GroceryItem(int id, string name, int quantity, DateTime expiryDate)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            ExpiryDate = expiryDate;
        }
    }

    // d. Generic Inventory Repository
    public class InventoryRepository<T> where T : IInventoryItem
    {
        private Dictionary<int, T> _items;

        public InventoryRepository()
        {
            _items = new Dictionary<int, T>();
        }

        public void AddItem(T item)
        {
            if (_items.ContainsKey(item.Id))
            {
                throw new DuplicateItemException($"Item with ID {item.Id} already exists in inventory.");
            }
            _items.Add(item.Id, item);
        }

        public T GetItemById(int id)
        {
            if (!_items.ContainsKey(id))
            {
                throw new ItemNotFoundException($"Item with ID {id} not found in inventory.");
            }
            return _items[id];
        }

        public void RemoveItem(int id)
        {
            if (!_items.ContainsKey(id))
            {
                throw new ItemNotFoundException($"Item with ID {id} not found in inventory.");
            }
            _items.Remove(id);
        }

        public List<T> GetAllItems()
        {
            return _items.Values.ToList();
        }

        public void UpdateQuantity(int id, int newQuantity)
        {
            if (newQuantity < 0)
            {
                throw new InvalidQuantityException($"Quantity cannot be negative. Provided: {newQuantity}");
            }

            if (!_items.ContainsKey(id))
            {
                throw new ItemNotFoundException($"Item with ID {id} not found in inventory.");
            }

            _items[id].Quantity = newQuantity;
        }
    }

    // f. WareHouseManager Class
    public class WareHouseManager
    {
        private InventoryRepository<ElectronicItem> _electronics;
        private InventoryRepository<GroceryItem> _groceries;

        public WareHouseManager()
        {
            _electronics = new InventoryRepository<ElectronicItem>();
            _groceries = new InventoryRepository<GroceryItem>();
        }

        public void SeedData()
        {
            // Add 2-3 items of each type
            _electronics.AddItem(new ElectronicItem(1, "Laptop", 10, "Dell", 24));
            _electronics.AddItem(new ElectronicItem(2, "Smartphone", 15, "Samsung", 12));
            _electronics.AddItem(new ElectronicItem(3, "Tablet", 8, "Apple", 18));

            _groceries.AddItem(new GroceryItem(101, "Rice", 50, DateTime.Now.AddMonths(6)));
            _groceries.AddItem(new GroceryItem(102, "Milk", 30, DateTime.Now.AddDays(7)));
            _groceries.AddItem(new GroceryItem(103, "Bread", 25, DateTime.Now.AddDays(3)));
        }

        public void PrintAllItems<T>(InventoryRepository<T> repo) where T : IInventoryItem
        {
            var items = repo.GetAllItems();
            if (items.Count == 0)
            {
                Console.WriteLine("No items found.");
                return;
            }

            foreach (var item in items)
            {
                Console.WriteLine($"ID: {item.Id}, Name: {item.Name}, Quantity: {item.Quantity}");
                
                if (item is ElectronicItem electronic)
                {
                    Console.WriteLine($"  Brand: {electronic.Brand}, Warranty: {electronic.WarrantyMonths} months");
                }
                else if (item is GroceryItem grocery)
                {
                    Console.WriteLine($"  Expiry Date: {grocery.ExpiryDate:yyyy-MM-dd}");
                }
            }
        }

        public void IncreaseStock<T>(InventoryRepository<T> repo, int id, int quantity) where T : IInventoryItem
        {
            try
            {
                var item = repo.GetItemById(id);
                repo.UpdateQuantity(id, item.Quantity + quantity);
                Console.WriteLine($"Successfully increased stock for item {id} by {quantity} units.");
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (InvalidQuantityException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void RemoveItemById<T>(InventoryRepository<T> repo, int id) where T : IInventoryItem
        {
            try
            {
                repo.RemoveItem(id);
                Console.WriteLine($"Successfully removed item with ID {id}.");
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void AddItem<T>(InventoryRepository<T> repo, T item) where T : IInventoryItem
        {
            try
            {
                repo.AddItem(item);
                Console.WriteLine($"Successfully added item: {item.Name} (ID: {item.Id})");
            }
            catch (DuplicateItemException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void UpdateItemQuantity<T>(InventoryRepository<T> repo, int id, int newQuantity) where T : IInventoryItem
        {
            try
            {
                repo.UpdateQuantity(id, newQuantity);
                Console.WriteLine($"Successfully updated quantity for item {id} to {newQuantity}.");
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (InvalidQuantityException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Getters for repositories
        public InventoryRepository<ElectronicItem> Electronics => _electronics;
        public InventoryRepository<GroceryItem> Groceries => _groceries;
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            // i. Instantiate WareHouseManager
            var warehouseManager = new WareHouseManager();

            // ii. Call SeedData()
            warehouseManager.SeedData();

            // iii. Print all grocery items
            Console.WriteLine("=== All Grocery Items ===");
            warehouseManager.PrintAllItems(warehouseManager.Groceries);
            Console.WriteLine();

            // iv. Print all electronic items
            Console.WriteLine("=== All Electronic Items ===");
            warehouseManager.PrintAllItems(warehouseManager.Electronics);
            Console.WriteLine();

            // v. Try to trigger exceptions and handle them gracefully
            Console.WriteLine("=== Testing Exception Handling ===");

            // Try to add a duplicate item
            Console.WriteLine("1. Testing DuplicateItemException:");
            warehouseManager.AddItem(warehouseManager.Electronics, new ElectronicItem(1, "Duplicate Laptop", 5, "HP", 12));
            Console.WriteLine();

            // Try to remove a non-existent item
            Console.WriteLine("2. Testing ItemNotFoundException (Remove):");
            warehouseManager.RemoveItemById(warehouseManager.Electronics, 999);
            Console.WriteLine();

            // Try to update with invalid quantity
            Console.WriteLine("3. Testing InvalidQuantityException:");
            warehouseManager.UpdateItemQuantity(warehouseManager.Groceries, 101, -5);
            Console.WriteLine();

            // Try to get a non-existent item
            Console.WriteLine("4. Testing ItemNotFoundException (Get):");
            try
            {
                var nonExistentItem = warehouseManager.Electronics.GetItemById(999);
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine();

            // Demonstrate successful operations
            Console.WriteLine("=== Successful Operations ===");
            warehouseManager.IncreaseStock(warehouseManager.Electronics, 1, 5);
            warehouseManager.UpdateItemQuantity(warehouseManager.Groceries, 101, 60);
            warehouseManager.RemoveItemById(warehouseManager.Electronics, 3);
        }
    }
}
