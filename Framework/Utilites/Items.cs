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
        private readonly HttpClient _httpClient;
        public Items(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public static Dictionary<string, Item> items = new();

        //Load Json into Dictionary<string, Item>
        
        public async Task LoadItemsAsync(string path = "items.json")
        {
            var jsonUtility = new JsonUtility(_httpClient);
            items = await jsonUtility.LoadFromJsonAsync<Dictionary<string, Item>>(path);
        }
        public Item GetPropertiesByName(string ItemName) //used to be propertyName, don't know why
        {
            // Make sure items.json has been read
            if (items.Count == 0)
            {
                throw new Exception("No items read, Call LoadItemsAsync() first.");
            }
            
            Console.WriteLine(items[ItemName].Description);
            Console.WriteLine(items[ItemName].Image);
            return items[ItemName];

        }

        public List<Item> GetPropertiesByList(List<string> inventory)
        {   
            List<Item> InventoryItemsList = new();

            foreach (string ItemName in inventory)
            {
                InventoryItemsList.Add(GetPropertiesByName(ItemName));
            }

            return InventoryItemsList;
        }

    }

}