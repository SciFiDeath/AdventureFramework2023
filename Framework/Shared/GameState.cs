using Microsoft.AspNetCore.Components;

using Encryption;
using JsonUtilities;
using FrameworkItems;
using static InventoryEvent;
using Microsoft.JSInterop;


//Notifications
using Blazored.Toast.Services;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace GameStateInventory;

public class GameState
{
	private readonly IToastService ToastService;

	private readonly EncryptionService Encryption;

	protected JsonUtility JsonUtility { get; set; } = null!;

	protected Items Items { get; set; }
	//Initialize Inventory
	private static List<string> ItemsInInventory = new();

	public static Dictionary<string, bool> State = new();

	public GameState(JsonUtility jsonUtility, Items items, IToastService toastService, EncryptionService encryption)
	{
		JsonUtility = jsonUtility;
		Items = items;
		ToastService = toastService;

		Encryption = encryption;
	}

	public async Task LoadGameStateAndItemsAsync(string path = "gamestate.json")
	{
		State = await JsonUtility.LoadFromJsonAsync<Dictionary<string, bool>>(path);
		await Items.LoadItemsAsync();

	}

	public void ChangeVisibility(string name)
	{
		State[name] = !State[name];
	}

	public bool CheckVisibility(string name)
	{
		try
		{
			return State[name];
		}
		catch (KeyNotFoundException)
		{
			// Everything is visible by default
			return true;
		}
	}

	// INVENTORY PART
	public void RemoveItem(string id)
	{
		bool removed = ItemsInInventory.Remove(id);

		if (!removed)
		{
			throw new ArgumentException($"Element {id} is not in Inventory");
		}
		Console.WriteLine($"Successfully removed {id} from inventory");
	}

    public void AddItem(string id)
    {

        if (Items.DoesItemExist(id) == false)
        {
            throw new Exception("Item doesn't exist in items.json Dictionary");
        }
        ItemsInInventory.Add(id);

        ToastService.ShowSuccess($"Added {id} to inventory");

        //Event handler for updateing inventory images
        InventoryEvent.OnItemAdded(this, new ItemAddedEventArgs { ItemId = id });
    }

    public bool CheckForItem(string id)
	{
		return ItemsInInventory.Contains(id);
	}
	
	public Dictionary<string, Item> GetItemObjects()
	{
        Dictionary<string, Item> ItemObjects = new();

		foreach (string id in ItemsInInventory)
		{	
			if (Items.items.ContainsKey(id))
			{
				ItemObjects.Add(id, Items.items[id]);
			}
			
		}

		return ItemObjects;
	}

    public async Task<string> CreateSaveString()
	{
		string serializedJson = JsonSerializer.Serialize(State);
		return await Encryption.EncryptString(serializedJson, "1234");

	}



}

