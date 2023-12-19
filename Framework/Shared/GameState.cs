using Microsoft.AspNetCore.Components;

using JsonUtilities;
using FrameworkItems;

//Notifications
using Blazored.Toast.Services;
using System.Runtime.CompilerServices;

namespace GameStateInventory;

public class GameState
{
	private readonly IToastService ToastService;
	protected JsonUtility JsonUtility { get; set; } = null!;

	protected Items Items { get; set; }
	//Initialize Inventory
	private static List<string> ItemsInInventory = new();

	public static Dictionary<string, bool> State = new();

	public GameState(JsonUtility jsonUtility, Items items, IToastService toastService)
	{
		JsonUtility = jsonUtility;
		Items = items;
		ToastService = toastService;
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

	public void RemoveItem(string id)
	{
		bool removed = ItemsInInventory.Remove(id);

		if (!removed)
		{
			throw new ArgumentException($"Element {id} is not in Inventory");
		}
		Console.WriteLine($"Successfully removed {id} from inventory");
	}

	public async void AddItem(string id)
	{

		if (Items.DoesItemExist(id) == false)
		{
			throw new Exception("Item doesn't exist in items.json Dictionary");
		}
		ItemsInInventory.Add(id);
		ToastService.ShowSuccess($"Added {id} to inventory");
		Console.WriteLine($"Successfully added {id} to inventory");
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

