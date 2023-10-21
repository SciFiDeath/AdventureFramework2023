public class Inventory
{
    // Define the items list as a static field to make it accessible to all instances of the class.
    private static List<string> items = new List<string>();

    public static void RemoveItem(string id)
    {
        bool removed = items.Remove(id);

        if (!removed)
        {
            throw new ArgumentException($"Element {id} is not in Inventory");
        }

        // Save the updated inventory to the file.
        SaveToFile();
    }

    public static void AddItem(string id)
    {
        items.Add(id);

        // Save the updated inventory to the file.
        SaveToFile();
    }

    public static List<string> GetItems()
    {
        return items;
    }

    public static void SaveToFile()
    {
        JsonUtility.SaveToJson<List<string>>(items, "inventory.json");
    }

    public static void LoadFromFile()
    {
        JsonUtility.LoadFromJson<List<string>>("inventory.json");
    }
}
