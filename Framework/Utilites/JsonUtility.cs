using System.Text.Json;
namespace JsonUtilities
{
    public class JsonUtility
    {
        public static T LoadFromJson<T>(string fileName)
        {   
            Console.WriteLine(fileName);
            Console.WriteLine(File.Exists(fileName));
            //TODO Use http for fetching json, jona & laurin
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                Console.WriteLine("printing json");
                Console.WriteLine(json);

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