using System.Text.Json;
namespace JsonUtilities
{
    public class JsonUtility
    {
        public static T LoadFromJson<T>(string fileName)
        {
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                return JsonSerializer.Deserialize<T>(json);
            }
            return default;
        }

        public static void SaveToJson<T>(T data, string fileName)
        {
            string json = JsonSerializer.Serialize(data);
            File.WriteAllText(fileName, json);
        }
    }
}