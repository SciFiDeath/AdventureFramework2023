using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JsonUtilities;

namespace InventoryItems
{
    public class Inventory
    {
        private readonly HttpClient _httpClient;
        private static List<string> inventoryItems = new();

        public Inventory(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void RemoveItem(string id)
        {
            bool removed = inventoryItems.Remove(id);

            if (!removed)
            {
                throw new ArgumentException($"Element {id} is not in Inventory");
            }

            // Save the updated inventory to the file.
            SaveInventory();
        }

        public void AddItem(string id)
        {
            inventoryItems.Add(id);

            // Save the updated inventory to the file.
            SaveInventory();
        }

        public List<string> GetItems()
        {
            return inventoryItems;
        }

        public async Task LoadInventoryAsync(string path = "inventory.json")
        {   
            Console.WriteLine("start LoadInventory");
            var jsonUtility = new JsonUtility(_httpClient);
            inventoryItems = await jsonUtility.LoadFromJsonAsync<List<string>>(path);
            Console.WriteLine("end of LoadInventory");
        }

        public /*async*/ void SaveInventory(string path = "inventory.json")
        {
            var jsonUtility = new JsonUtility(_httpClient);
            //TODO: This doesn't work yet because i will do saving the file later
            //await jsonUtility.SaveToJsonAsync(items, path);
        }

        public List<Item> GetPropertiesByList()
        {   
            Console.WriteLine("start function");
            List<Item> InventoryItemsList = new();

            Items items = new Items(_httpClient);

            Console.WriteLine("before for loop");
            foreach (string ItemName in inventoryItems)
            {
                InventoryItemsList.Add(items.GetPropertiesByName(ItemName));
                Console.WriteLine("each GetPropertiesByName");
            }

            return InventoryItemsList;
        }
    }
}
