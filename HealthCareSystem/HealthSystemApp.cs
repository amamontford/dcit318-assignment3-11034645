using System;
using System.Collections.Generic;
using HealthCareSystem.Models;

namespace HealthCareSystem
{
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
            _patientRepo.Add(new Patient(1, "Ama Montford", 25, "Female"));
            _patientRepo.Add(new Patient(2, "Christian Agyapong", 38, "Male"));
            _patientRepo.Add(new Patient(3, "Nhyira Yawson", 28, "Female"));

            _prescriptionRepo.Add(new Prescription(1, 1, "Aspirin", DateTime.Now.AddDays(-30)));
            _prescriptionRepo.Add(new Prescription(2, 1, "Ibuprofen", DateTime.Now.AddDays(-15)));
            _prescriptionRepo.Add(new Prescription(3, 2, "Amoxicillin", DateTime.Now.AddDays(-20)));
            _prescriptionRepo.Add(new Prescription(4, 2, "Vitamin C", DateTime.Now.AddDays(-10)));
            _prescriptionRepo.Add(new Prescription(5, 3, "Nugel", DateTime.Now.AddDays(-5)));
        }

        public void BuildPrescriptionMap()
        {
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

        public List<Prescription> GetPrescriptionsByPatientId(int patientId)
        {
            if (_prescriptionMap.ContainsKey(patientId))
            {
                return _prescriptionMap[patientId];
            }
            return new List<Prescription>();
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

        public void SelectAndDisplayPatientPrescriptions()
        {
            bool validInput = false;

            while (!validInput)
            {
                Console.Write("Enter a Patient ID to view prescriptions (or 'q' to quit): ");
                string input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input) || input.ToLower() == "q")
                {
                    Console.WriteLine("Exiting...");
                    return;
                }

                if (int.TryParse(input, out int selectedPatientId))
                {
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
                        foreach (var p in _patientRepo.GetAll())
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
}
