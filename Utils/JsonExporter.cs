using System.Text.Json;
using UserData.Models;

namespace UserData.Utils
{
    public static class JsonExporter
    {
        public static void ExportToJson(string filePath, List<User> users)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true 
            };

            string jsonString = JsonSerializer.Serialize(users, options);

            File.WriteAllText(filePath, jsonString);
        }
    }
}
