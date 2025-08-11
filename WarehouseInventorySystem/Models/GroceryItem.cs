using WarehouseInventorySystem.Interfaces;

namespace WarehouseInventorySystem.Models
{
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
}
