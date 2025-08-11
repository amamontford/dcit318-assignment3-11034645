namespace HealthCareSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var healthApp = new HealthSystemApp();

            // Add initial data
            healthApp.SeedData();

            // Build mapping between patients and their prescriptions
            healthApp.BuildPrescriptionMap();

            // Show all patients
            healthApp.PrintAllPatients();

            // Allow user to choose a patient and view prescriptions
            healthApp.SelectAndDisplayPatientPrescriptions();
        }
    }
}
