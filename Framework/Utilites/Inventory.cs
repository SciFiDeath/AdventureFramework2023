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
        private static List<string> items = new();

        public Inventory(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void RemoveItem(string id)
        {
            bool removed = items.Remove(id);

            if (!removed)
            {
                throw new ArgumentException($"Element {id} is not in Inventory");
            }

            // Save the updated inventory to the file.
            SaveInventory();
        }

        public void AddItem(string id)
        {
            items.Add(id);

            // Save the updated inventory to the file.
            SaveInventory();
        }

        public List<string> GetItems()
        {
            return items;
        }

        public async Task LoadInventoryAsync(string path = "inventory.json")
        {
            var jsonUtility = new JsonUtility(_httpClient);
            items = await jsonUtility.LoadFromJsonAsync<List<string>>(path);
        }

        public /*async*/ void SaveInventory(string path = "inventory.json")
        {
            var jsonUtility = new JsonUtility(_httpClient);
            //TODO: This doesn't work yet because i will do saving the file later
            //await jsonUtility.SaveToJsonAsync(items, path);
        }
    }
}
