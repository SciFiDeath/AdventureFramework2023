using System.Net.Http.Json;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;

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

    public string EncryptGameStateInventory(Dictionary<string, bool> boolDict, List<string> stringList, string key)
{
    Console.WriteLine("starting encryption");
    // Convert dictionary and list to JSON
    string jsonData = JsonSerializer.Serialize(new { BoolDict = boolDict, StringList = stringList });

    Console.WriteLine("serializing game state");
    // Encrypt the JSON string using Rijndael
    using (Rijndael rijAlg = Rijndael.Create())
    {
        rijAlg.Key = Encoding.UTF8.GetBytes(key);
        rijAlg.GenerateIV();

        ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

        using (MemoryStream msEncrypt = new MemoryStream())
        {
            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(jsonData);
                }
            }

            byte[] encryptedBytes = msEncrypt.ToArray();
            byte[] combinedBytes = new byte[rijAlg.IV.Length + encryptedBytes.Length];
            Array.Copy(rijAlg.IV, 0, combinedBytes, 0, rijAlg.IV.Length);
            Array.Copy(encryptedBytes, 0, combinedBytes, rijAlg.IV.Length, encryptedBytes.Length);

            // Return the encrypted data
            Console.WriteLine("returning encryptedData");
            return Convert.ToBase64String(combinedBytes);
        }
    }
}



    static (Dictionary<string, bool> BoolDict, List<string> StringList) DecryptData(string encryptedData, string key)
    {
        byte[] combinedBytes = Convert.FromBase64String(encryptedData);

        using (Aes aesAlg = Aes.Create())
        {
            int ivSize = aesAlg.BlockSize / 8;
            byte[] iv = new byte[ivSize];
            byte[] encryptedBytes = new byte[combinedBytes.Length - ivSize];

            Array.Copy(combinedBytes, 0, iv, 0, ivSize);
            Array.Copy(combinedBytes, ivSize, encryptedBytes, 0, encryptedBytes.Length);

            aesAlg.Key = Encoding.UTF8.GetBytes(key);
            aesAlg.IV = iv;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        string decryptedJson = srDecrypt.ReadToEnd();

                        // Deserialize the JSON string to the desired type
                        return JsonSerializer.Deserialize<(Dictionary<string, bool> BoolDict, List<string> StringList)>(decryptedJson);
                    }
                }
            }
        }
    }
}
