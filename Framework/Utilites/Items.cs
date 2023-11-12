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
        public List<string> GetProperties(string propertyName)
        {
            // Make sure items.json has been read
            if (items.Count == 0)
            {
                throw new Exception("No items read, Call LoadItemsAsync() first.");
            }
            Console.WriteLine("items to be printed");
            
            
            for(int x=0; x< items.Count; x++)
            {
                Console.WriteLine("{0} and {1}", items.Keys.ElementAt(x), 
                                    items[ items.Keys.ElementAt(x)]);
            }

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