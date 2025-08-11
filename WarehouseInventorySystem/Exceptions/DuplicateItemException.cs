using System;

namespace WarehouseInventorySystem.Exceptions
{
    public class DuplicateItemException : Exception
    {
        public DuplicateItemException(string message) : base(message) { }
    }
}
