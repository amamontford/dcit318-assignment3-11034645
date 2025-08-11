using System;
using InventoryRecordSystem.Interfaces;

namespace InventoryRecordSystem.Models
{
    public record InventoryItem(int Id, string Name, int Quantity, DateTime DateAdded) : IInventoryEntity;
}
