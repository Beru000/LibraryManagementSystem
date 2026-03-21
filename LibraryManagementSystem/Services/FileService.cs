using System.Text.Json;

namespace LibraryManagementSystem.Services
{
    public class FileService
    {
        private readonly string _dataPath = "Data";

        public async Task SaveAsync<T>(string fileName, List<T> data)
        {
            string filePath = Path.Combine(_dataPath, fileName);
            if (!Directory.Exists(_dataPath))
            {
                Directory.CreateDirectory(_dataPath);
            } 
            string json = JsonSerializer.Serialize(data);
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task<List<T>> LoadAsync<T>(string fileName)
        {
            var result = new List<T>(); 

            string filePath = Path.Combine(_dataPath, fileName);
            if (File.Exists(filePath))
            {
                string content = await File.ReadAllTextAsync(filePath);
                var collection = JsonSerializer.Deserialize<List<T>>(content) ?? new List<T>();

                result = collection;
            }

            return result;
        }
    }
}
