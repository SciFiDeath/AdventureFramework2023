
namespace JsonUtilities
{
    public class Inventory
    {
        // Define the items list as a static field to make it accessible to all instances of the class.
        private static List<string> inventory = new List<string>();

        public static void RemoveItem(string id)
        {
            bool removed = inventory.Remove(id);

            if (!removed)
            {
                throw new ArgumentException($"Element {id} is not in Inventory");
            }

            // Save the updated inventory to the file.
            SaveInventory();
        }

        public static void AddItem(string id)
        {
            inventory.Add(id);

            // Save the updated inventory to the file.
            SaveInventory();
        }

        public static List<string> GetItems()
        {
            return inventory;
        }

        public static void SaveInventory(string path="inventory.json")
        {
            JsonUtility.SaveToJson<List<string>>(inventory, "inventory.json");
        }

        public static void LoadInventory(string path="inventory.json", bool overwrite = false)
        {   
            if (inventory.Count == 0 || overwrite == true)
            {
                inventory = JsonUtility.LoadFromJson<List<string>>(path);
            }

            else
            {
                throw new ArgumentException("Inventory is not Empty and would be overwritten by loading. Set overwrite to true to overwrite");
            }
        }
    }

}



