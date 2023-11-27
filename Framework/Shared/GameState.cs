using Microsoft.AspNetCore.Components;

using JsonUtilities;

namespace InventoryItems;

public class GameState
{
    [Inject]
	protected JsonUtility JsonUtility { get; set; } = null!;
    private static List<string> ItemsInInventory = new();

    private static Dictionary<string, bool> State = new();
    
    public async Task LoadGameStateAsync(string path = "gamestate.json")
    {
        State = await JsonUtility.LoadFromJsonAsync<Dictionary<string, bool>>(path);
    }

    
    public void RemoveItem(string id)
    {
        bool removed = ItemsInInventory.Remove(id);

        if (!removed)
        {
            throw new ArgumentException($"Element {id} is not in Inventory");
        }

    }

    public void AddItem(string id)
    {
        ItemsInInventory.Add(id);
    }
    public bool CheckForItem(string id)
    {
        return ItemsInInventory.Contains(id);
    }
    public List<string> GetItems()
    {
        return ItemsInInventory;
    }

}

