using System;
using System.Collections.Generic;
using System.IO;

namespace SchoolGradingSystem
{
    public class InvalidScoreFormatException : Exception
    {
        public InvalidScoreFormatException(string message) : base(message) { }
    }

    public class MissingFieldException : Exception
    {
        public MissingFieldException(string message) : base(message) { }
    }

    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Score { get; set; }

        public Student(int id, string fullName, int score)
        {
            Id = id;
            FullName = fullName;
            Score = score;
        }

        public string GetGrade()
        {
            if (Score >= 80 && Score <= 100)
                return "A";
            else if (Score >= 70 && Score <= 79)
                return "B";
            else if (Score >= 60 && Score <= 69)
                return "C";
            else if (Score >= 50 && Score <= 59)
                return "D";
            else
                return "F";
        }
    }

    public class StudentResultProcessor
    {
        public List<Student> ReadStudentsFromFile(string inputFilePath)
        {
            var students = new List<Student>();

            using (StreamReader reader = new StreamReader(inputFilePath))
            {
                string line;
                int lineNumber = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;
                    string trimmedLine = line.Trim();

                    if (string.IsNullOrEmpty(trimmedLine))
                        continue;

                    string[] fields = trimmedLine.Split(',');

                    if (fields.Length != 3)
                        throw new MissingFieldException($"Line {lineNumber}: Expected 3 fields (ID, FullName, Score), but found {fields.Length}.");

                    if (!int.TryParse(fields[0].Trim(), out int id))
                        throw new InvalidScoreFormatException($"Line {lineNumber}: Invalid ID format '{fields[0].Trim()}'.");

                    string fullName = fields[1].Trim();
                    if (string.IsNullOrEmpty(fullName))
                        throw new MissingFieldException($"Line {lineNumber}: Full name cannot be empty.");

                    if (!int.TryParse(fields[2].Trim(), out int score))
                        throw new InvalidScoreFormatException($"Line {lineNumber}: Invalid score format '{fields[2].Trim()}'.");

                    if (score < 0 || score > 100)
                        throw new InvalidScoreFormatException($"Line {lineNumber}: Score must be between 0 and 100, but found {score}.");

                    students.Add(new Student(id, fullName, score));
                }
            }

            return students;
        }

        public void WriteReportToFile(List<Student> students, string outputFilePath)
        {
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                writer.WriteLine("=== Student Grade Report ===");
                writer.WriteLine($"Generated on: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                writer.WriteLine();

                foreach (var student in students)
                {
                    string grade = student.GetGrade();
                    writer.WriteLine($"{student.FullName} (ID: {student.Id}): Score = {student.Score}, Grade = {grade}");
                }

                writer.WriteLine();
                writer.WriteLine($"Total Students: {students.Count}");
            }
        }
    }

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
            catch (MissingFieldException ex)
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
