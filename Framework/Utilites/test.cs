using InventoryItems;

class Test
{
    static void Main(string[] args)
    {
        Inventory inventory = new();
        List<string> InventoryItems = inventory.GetItems();
        foreach (var item in InventoryItems)
        {
            Console.WriteLine(item);
        } 
    }
}
