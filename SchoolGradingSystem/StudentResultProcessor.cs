using System;
using System.Collections.Generic;
using System.IO;
using SchoolGradingSystem.Models;
using SchoolGradingSystem.Exceptions;

namespace SchoolGradingSystem
{
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
                        throw new MissingStudentFieldException($"Line {lineNumber}: Expected 3 fields (ID, FullName, Score), but found {fields.Length}.");

                    if (!int.TryParse(fields[0].Trim(), out int id))
                        throw new InvalidScoreFormatException($"Line {lineNumber}: Invalid ID format '{fields[0].Trim()}'.");

                    string fullName = fields[1].Trim();
                    if (string.IsNullOrEmpty(fullName))
                        throw new MissingStudentFieldException($"Line {lineNumber}: Full name cannot be empty.");

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
}
