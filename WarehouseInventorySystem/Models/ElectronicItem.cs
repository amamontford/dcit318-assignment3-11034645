using WarehouseInventorySystem.Interfaces;

namespace WarehouseInventorySystem.Models
{
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
}
