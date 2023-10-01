
using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

private class Item
{
    public string name { get; set; }
    public string description { get; set; }
    public string image { get; set; }
}
public class Items
{   
    
    public static void JsonToDict(string path = "items.json")
    {
        string Path = path;

        try
        {
            // Read JSON data from the file into a string.
            string jsonString = File.ReadAllText(Path);

            // Deserialize the JSON data into a dictionary where the keys are integers.
            Dictionary<int, Item> items = JsonSerializer.Deserialize<Dictionary<int, Item>>(jsonString);

            foreach (var kvp in items)
            {
                int key = kvp.Key;
                Item item = kvp.Value;

                Console.WriteLine($"Key: {key}");
                Console.WriteLine($"Name: {item.name}");
                Console.WriteLine($"Description: {item.description}");
                Console.WriteLine($"Image: {item.image}");
            }
        }

        catch (FileNotFoundException)
        {
            Console.WriteLine("The JSON file was not found.");
        }

    }
}
Items.JsonToDict();