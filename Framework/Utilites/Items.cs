using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace InventoryLibrary
{
    public class Item
    {
        public string Name { get; }
        public string Description { get; }
        public string Image { get; }

        public Item(string name, string description, string image)
        {
            this.Name = name;
            this.Description = description;
            this.Image = image;
        }
    }

    public class Items
    {
        public static void JsonToDict(string path = "items.json")
        {
            string Path = path;

            try
            {
                string jsonString = File.ReadAllText(Path);
                Dictionary<int, Item> items = JsonSerializer.Deserialize<Dictionary<int, Item>>(jsonString);

                foreach (var kvp in items)
                {
                    int key = kvp.Key;
                    Item item = kvp.Value;

                    Console.WriteLine($"Key: {key}");
                    Console.WriteLine($"Name: {item.Name}");
                    Console.WriteLine($"Description: {item.Description}");
                    Console.WriteLine($"Image: {item.Image}");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("The JSON file was not found.");
            }
        }
    }
}
