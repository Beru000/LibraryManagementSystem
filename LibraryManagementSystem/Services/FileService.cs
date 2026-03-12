using System.Text.Json;

namespace LibraryManagementSystem.Services
{
    public class FileService
    {
        private readonly string _dataPath = "Data";

        public async Task SaveAsync<T>(string fileName, List<T> data)
        {
            string filePath = Path.Combine(_dataPath, fileName);
            Directory.CreateDirectory(_dataPath);
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(data, options);
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task<List<T>> LoadAsync<T>(string fileName)
        {
            string filePath = Path.Combine(_dataPath, fileName);
            if (File.Exists(filePath))
            {
                string content = await File.ReadAllTextAsync(filePath);
                return JsonSerializer.Deserialize<List<T>>(content) ?? new List<T>();
            }
            else
            {
                return new List<T>();
            }
        }
    }
}
