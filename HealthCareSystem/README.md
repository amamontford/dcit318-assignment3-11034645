# HealthCareSystem

A C# console application demonstrating generic classes, collections, and the repository pattern for managing patients and prescriptions.

## Project Structure

```
HealthCareSystem/
├── Program.cs                 # Main application with all classes
└── HealthCareSystem.csproj    # Project file
```

## Features

### Generic Repository Pattern
- **Repository<T>**: Generic class for entity storage and retrieval
- Methods: Add, GetAll, GetById, Remove

### Entity Classes
- **Patient**: Represents patient data (Id, Name, Age, Gender)
- **Prescription**: Represents prescription data (Id, PatientId, MedicationName, DateIssued)

### Collections Management
- **Dictionary<int, List<Prescription>>**: Groups prescriptions by PatientId
- **GetPrescriptionsByPatientId()**: Retrieves prescriptions for a specific patient

### Main Application
- **HealthSystemApp**: Orchestrates the entire system
- **SeedData()**: Populates sample data
- **BuildPrescriptionMap()**: Groups prescriptions by patient
- **PrintAllPatients()**: Displays all patients
- **PrintPrescriptionsForPatient()**: Shows prescriptions for a specific patient

## How to Run

1. Ensure you have .NET 6.0 or later installed
2. Navigate to the HealthCareSystem directory
3. Run the following commands:
   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

## Sample Output

The application will demonstrate:
- Creating and managing patients and prescriptions
- Using generic repositories for data storage
- Grouping prescriptions by patient using a dictionary
- Retrieving and displaying patient-specific prescription data
