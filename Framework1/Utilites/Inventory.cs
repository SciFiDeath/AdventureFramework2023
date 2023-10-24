using System;
using System.Collections.Generic;

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
    }

    public static void AddItem(string id)
    {
        items.Add(id);
    }

    public static List<string> GetItems()
    {
        return items;
    }
}
