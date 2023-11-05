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
        public static void LoadItems()
        {
            items = JsonUtility.LoadFromJson<Dictionary<string, Item>>("items.json");
        }
        
        public List<string> GetProperties(string propertyName)
        {
            // Make sure items.json has been read
            if (items.Count == 0)
            {
                LoadItems();
            }
            Console.WriteLine("items to be printed");
            Console.WriteLine(items);
            // Declare a list of property values
            List<string> propertyValues = new();

            //iterate through pairs in items
            foreach (KeyValuePair<string, Item> pair in items)
            {
                //get info about property for getting Property value using parameter
                var propertyInfo = pair.Value.GetType().GetProperty(propertyName);
                if (propertyInfo != null)
                {
                    // Get the value of the property and add it to the list
                    string? propertyValue = propertyInfo.GetValue(pair.Value) as string;
                    propertyValues.Add(propertyValue);
                }
                else
                {
                    // Handle the case where the property with the given name doesn't exist
                    // You can choose to skip, log an error, or handle it as needed.
                }
            }

            return propertyValues;
        }

    }

}