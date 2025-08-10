# Finance Management System

A C# console application demonstrating object-oriented programming concepts including records, interfaces, inheritance, and polymorphism.

## Project Structure

```
FinanceManagementSystem/
├── Program.cs                 # Main entry point
├── FinanceApp.cs             # Main application class
├── FinanceManagementSystem.csproj
├── Models/
│   └── Transaction.cs        # Transaction record type
├── Interfaces/
│   └── ITransactionProcessor.cs  # Transaction processing interface
├── Processors/
│   ├── BankTransferProcessor.cs
│   ├── MobileMoneyProcessor.cs
│   └── CryptoWalletProcessor.cs
└── Accounts/
    ├── Account.cs            # Base account class
    └── SavingsAccount.cs     # Sealed savings account class
```

## Features

### Core Models (Records)
- **Transaction**: Represents financial data with Id, Date, Amount, and Category

### Payment Behavior (Interfaces)
- **ITransactionProcessor**: Interface for processing transactions
- Three concrete implementations:
  - **BankTransferProcessor**: Processes bank transfers
  - **MobileMoneyProcessor**: Processes mobile money payments
  - **CryptoWalletProcessor**: Processes cryptocurrency transactions

### Account Management
- **Account**: Base class with AccountNumber and Balance properties
- **SavingsAccount**: Sealed class that inherits from Account with balance validation

### System Integration
- **FinanceApp**: Main application class that demonstrates all features
- Creates sample transactions and processes them using different payment methods
- Applies transactions to a savings account with balance validation

## How to Run

1. Ensure you have .NET 6.0 or later installed
2. Navigate to the FinanceManagementSystem directory
3. Run the following commands:
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

## Sample Output

The application will demonstrate:
- Creating a savings account with initial balance
- Processing transactions using different payment methods
- Applying transactions to the account with balance validation
- Displaying transaction summaries and final account balance
