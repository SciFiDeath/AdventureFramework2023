using JsonUtilities;

Inventory invent = new Inventory();
invent.LoadInventory();
List<string> inv = inventory.GetItems();

foreach (string item in inv)
{
    Console.WriteLine(item);
}
