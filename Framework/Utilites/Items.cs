using System.Dynamic;
using System.Text.Json;
using JsonUtilities;

namespace InventoryItems
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
        public static Dictionary<string, Item> items = new();

        //Load Json into Dictionary<string, Item>
        //TODO create a method that returns a list of item properties like images or descriptions
        public static void LoadItems()
        {
            Dictionary<string, Item> items = JsonUtility.LoadFromJson<Dictionary<string, Item>>("items.json");
        }
        
    }

}