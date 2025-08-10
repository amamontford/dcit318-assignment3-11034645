using System;
using System.Collections.Generic;
using System.Linq;

namespace HealthCareSystem
{
    // a. Generic Repository for Entity Management
    public class Repository<T>
    {
        private List<T> items;

        public Repository()
        {
            items = new List<T>();
        }

        public void Add(T item)
        {
            items.Add(item);
        }

        public List<T> GetAll()
        {
            return items;
        }

        public T? GetById(Func<T, bool> predicate)
        {
            return items.FirstOrDefault(predicate);
        }

        public bool Remove(Func<T, bool> predicate)
        {
            var item = items.FirstOrDefault(predicate);
            if (item != null)
            {
                return items.Remove(item);
            }
            return false;
        }
    }

    // b. Patient Class
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        public Patient(int id, string name, int age, string gender)
        {
            Id = id;
            Name = name;
            Age = age;
            Gender = gender;
        }
    }

    // c. Prescription Class
    public class Prescription
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string MedicationName { get; set; }
        public DateTime DateIssued { get; set; }

        public Prescription(int id, int patientId, string medicationName, DateTime dateIssued)
        {
            Id = id;
            PatientId = patientId;
            MedicationName = medicationName;
            DateIssued = dateIssued;
        }
    }

    // g. HealthSystemApp Class
    public class HealthSystemApp
    {
        private Repository<Patient> _patientRepo;
        private Repository<Prescription> _prescriptionRepo;
        private Dictionary<int, List<Prescription>> _prescriptionMap;

        public HealthSystemApp()
        {
            _patientRepo = new Repository<Patient>();
            _prescriptionRepo = new Repository<Prescription>();
            _prescriptionMap = new Dictionary<int, List<Prescription>>();
        }

        public void SeedData()
        {
            // Add 2-3 Patient objects to the patient repository
            _patientRepo.Add(new Patient(1, "Ama Montford", 25, "Female"));
            _patientRepo.Add(new Patient(2, "Christian Agyapong", 38, "Male"));
            _patientRepo.Add(new Patient(3, "Nhyira Yawson", 28, "Female"));

            // Add 4-5 Prescription objects to the prescription repository (with valid PatientIds)
            _prescriptionRepo.Add(new Prescription(1, 1, "Aspirin", DateTime.Now.AddDays(-30)));
            _prescriptionRepo.Add(new Prescription(2, 1, "Ibuprofen", DateTime.Now.AddDays(-15)));
            _prescriptionRepo.Add(new Prescription(3, 2, "Amoxicillin", DateTime.Now.AddDays(-20)));
            _prescriptionRepo.Add(new Prescription(4, 2, "Vitamin C", DateTime.Now.AddDays(-10)));
            _prescriptionRepo.Add(new Prescription(5, 3, "Nugel", DateTime.Now.AddDays(-5)));
        }

        public void BuildPrescriptionMap()
        {
            // Loop through all prescriptions, group them by PatientId, and populate _prescriptionMap
            var allPrescriptions = _prescriptionRepo.GetAll();
            
            foreach (var prescription in allPrescriptions)
            {
                if (!_prescriptionMap.ContainsKey(prescription.PatientId))
                {
                    _prescriptionMap[prescription.PatientId] = new List<Prescription>();
                }
                _prescriptionMap[prescription.PatientId].Add(prescription);
            }
        }

        public void PrintAllPatients()
        {
            var patients = _patientRepo.GetAll();
            Console.WriteLine("=== All Patients ===");
            foreach (var patient in patients)
            {
                Console.WriteLine($"ID: {patient.Id}, Name: {patient.Name}, Age: {patient.Age}, Gender: {patient.Gender}");
            }
            Console.WriteLine();
        }

        public void PrintPrescriptionsForPatient(int patientId)
        {
            var prescriptions = GetPrescriptionsByPatientId(patientId);
            var patient = _patientRepo.GetById(p => p.Id == patientId);
            
            if (patient != null)
            {
                Console.WriteLine($"=== Prescriptions for {patient.Name} (ID: {patientId}) ===");
                if (prescriptions.Count > 0)
                {
                    foreach (var prescription in prescriptions)
                    {
                        Console.WriteLine($"Prescription ID: {prescription.Id}, Medication: {prescription.MedicationName}, Date Issued: {prescription.DateIssued:yyyy-MM-dd}");
                    }
                }
                else
                {
                    Console.WriteLine("No prescriptions found for this patient.");
                }
            }
            else
            {
                Console.WriteLine($"Patient with ID {patientId} not found.");
            }
            Console.WriteLine();
        }

        public List<Prescription> GetPrescriptionsByPatientId(int patientId)
        {
            if (_prescriptionMap.ContainsKey(patientId))
            {
                return _prescriptionMap[patientId];
            }
            return new List<Prescription>();
        }

        public void SelectAndDisplayPatientPrescriptions()
        {
            bool validInput = false;
            int selectedPatientId = 0;

            while (!validInput)
            {
                Console.Write("Enter a Patient ID to view prescriptions (or 'q' to quit): ");
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input) || input.ToLower() == "q")
                {
                    Console.WriteLine("Exiting...");
                    return;
                }

                if (int.TryParse(input, out selectedPatientId))
                {
                    // Check if the patient exists
                    var patient = _patientRepo.GetById(p => p.Id == selectedPatientId);
                    if (patient != null)
                    {
                        validInput = true;
                        PrintPrescriptionsForPatient(selectedPatientId);
                    }
                    else
                    {
                        Console.WriteLine($"Error: Patient with ID {selectedPatientId} does not exist.");
                        Console.WriteLine("Available Patient IDs:");
                        var allPatients = _patientRepo.GetAll();
                        foreach (var p in allPatients)
                        {
                            Console.WriteLine($"  - {p.Id}: {p.Name}");
                        }
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("Error: Please enter a valid number for Patient ID.");
                    Console.WriteLine();
                }
            }
        }
    }

    // Main Program
    class Program
    {
        static void Main(string[] args)
        {
            // i. Instantiate HealthSystemApp
            var healthApp = new HealthSystemApp();

            // ii. Call SeedData()
            healthApp.SeedData();

            // iii. Call BuildPrescriptionMap()
            healthApp.BuildPrescriptionMap();

            // iv. Print all patients
            healthApp.PrintAllPatients();

            // v. Allow user to select a PatientId and display prescriptions
            healthApp.SelectAndDisplayPatientPrescriptions();
        }
    }
}
