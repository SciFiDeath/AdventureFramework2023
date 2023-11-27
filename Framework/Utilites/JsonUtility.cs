using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Framework.Slides;
namespace JsonUtilities;

public class JsonUtility
{
    private readonly HttpClient _httpClient;

    public JsonUtility(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<T> LoadFromJsonAsync<T>(string fileName)
    {
        // assign return value from GetFromJsonAsync to output if it is not null, otherwise throw an exception
        var json = await _httpClient.GetFromJsonAsync<T>(fileName) ?? throw new Exception("GetFromJsonAsync is null");
        return json;
    }

    public static void SaveToJson<T>(T data, string fileName)
    {
        string json = JsonSerializer.Serialize(data);
        File.WriteAllText(fileName, json);
    }
}