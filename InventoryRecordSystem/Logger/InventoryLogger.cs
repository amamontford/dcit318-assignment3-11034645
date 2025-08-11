using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using InventoryRecordSystem.Interfaces;

namespace InventoryRecordSystem.Logger
{
    public class InventoryLogger<T> where T : IInventoryEntity
    {
        private readonly List<T> _log = new();
        private readonly string _filePath;

        public InventoryLogger(string filePath)
        {
            _filePath = filePath;
        }

        public void Add(T item) => _log.Add(item);

        public List<T> GetAll() => new(_log);

        public void SaveToFile()
        {
            try
            {
                string jsonData = JsonSerializer.Serialize(_log, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, jsonData);
                Console.WriteLine($"Data saved successfully to {_filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving data: {ex.Message}");
            }
        }

        public void LoadFromFile()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    Console.WriteLine("No existing data file found.");
                    return;
                }

                string jsonData = File.ReadAllText(_filePath);
                var loadedItems = JsonSerializer.Deserialize<List<T>>(jsonData);

                if (loadedItems != null)
                {
                    _log.Clear();
                    _log.AddRange(loadedItems);
                }

                Console.WriteLine($"Data loaded successfully from {_filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
        }
    }
}
