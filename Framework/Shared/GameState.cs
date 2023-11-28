using Microsoft.AspNetCore.Components;

using JsonUtilities;
using Items;

using System.Runtime.CompilerServices;

namespace GameStateInventory;

public class GameState
{

	protected JsonUtility JsonUtility { get; set; } = null!;

    protected Items.Items Items {get; set;} = null!;
    //Initialize Inventory
    private static List<string> ItemsInInventory = new();

    public static Dictionary<string, bool> State = new();

    public GameState(JsonUtility jsonUtility)
    {
        JsonUtility = jsonUtility;
    }
    
    public async Task LoadGameStateAsync(string path = "gamestate.json")
    {
        State = await JsonUtility.LoadFromJsonAsync<Dictionary<string, bool>>(path);
    }

    public void ChangeVisibility(string name)
    {
        State[name] = !State[name];
    }

    public bool CheckVisibility(string name)
    {
        return State[name];
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
        if (Items.DoesItemExist(id) == false)
        {
            throw new Exception("Item doesn't exist in items.json Dictionary");
        }
        //TODO FIX THIS
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

