using System.Text.Json;

namespace Utilities
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
        public static void LoadItems()
        {
            Dictionary<string, Item> Items = JsonUtility.LoadFromJson<Dictionary<string, Item>>("items.json");
        }
        
    }

}