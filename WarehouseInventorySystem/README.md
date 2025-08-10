# WarehouseInventorySystem

A C# console application demonstrating collections, generics, and exception handling for warehouse inventory management.

## Project Structure

```
WarehouseInventorySystem/
├── Program.cs                 # Main application with all classes
└── WarehouseInventorySystem.csproj    # Project file
```

## Features

### Marker Interface
- **IInventoryItem**: Interface that all inventory items must implement
- Properties: Id, Name, Quantity

### Product Classes
- **ElectronicItem**: Implements IInventoryItem with Brand and WarrantyMonths
- **GroceryItem**: Implements IInventoryItem with ExpiryDate

### Generic Repository
- **InventoryRepository<T>**: Generic repository where T : IInventoryItem
- Methods: AddItem, GetItemById, RemoveItem, GetAllItems, UpdateQuantity
- Uses Dictionary<int, T> for efficient storage and retrieval

### Custom Exceptions
- **DuplicateItemException**: Thrown when adding an item with existing ID
- **ItemNotFoundException**: Thrown when item is not found
- **InvalidQuantityException**: Thrown when quantity is negative

### Main Application
- **WareHouseManager**: Orchestrates the entire system
- **SeedData()**: Populates sample data
- **PrintAllItems()**: Displays all items in a repository
- **IncreaseStock()**: Increases item quantity
- **RemoveItemById()**: Removes items by ID
- **Exception handling**: Graceful error handling with user-friendly messages

## How to Run

1. Ensure you have .NET 6.0 or later installed
2. Navigate to the WarehouseInventorySystem directory
3. Run the following commands:
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

## Sample Output

The application will demonstrate:
- Creating and managing electronic and grocery items
- Using generic repositories for different item types
- Exception handling for various error scenarios
- Successful inventory operations
