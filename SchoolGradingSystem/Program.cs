using System;
using System.IO;
using SchoolGradingSystem.Exceptions;
using SchoolGradingSystem.Models;

namespace SchoolGradingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = "students.txt";
            string outputFilePath = "grade_report.txt";

            try
            {
                if (!File.Exists(inputFilePath))
                    throw new FileNotFoundException($"The file '{inputFilePath}' was not found. Please make sure it exists in the program directory.");

                var processor = new StudentResultProcessor();

                Console.WriteLine("Reading students from file...");
                var students = processor.ReadStudentsFromFile(inputFilePath);
                Console.WriteLine($"Successfully read {students.Count} students from file.");

                Console.WriteLine("Writing grade report to file...");
                processor.WriteReportToFile(students, outputFilePath);
                Console.WriteLine($"Grade report written to {outputFilePath}");

                Console.WriteLine("\n=== Grade Report ===");
                foreach (var student in students)
                {
                    Console.WriteLine($"{student.FullName} (ID: {student.Id}): Score = {student.Score}, Grade = {student.GetGrade()}");
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (InvalidScoreFormatException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (MissingStudentFieldException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }
    }
}
