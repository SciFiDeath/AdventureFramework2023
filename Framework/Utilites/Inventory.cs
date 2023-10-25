using JsonUtilities;

namespace InventoryItems
{
    public class Inventory
    {
        // Define the items list as a static field to make it accessible to all instances of the class.
        private static readonly List<string> items = new(); //? public for every file to access, like inventory overlay or mini-games

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

        public static void SaveInventory(string path="inventory.json")
        {
            JsonUtility.SaveToJson(items, path);
        }

        public static void LoadInventory(string path="inventory.json" )
        {
            JsonUtility.LoadFromJson<List<string>>(path);
        }
    }
}