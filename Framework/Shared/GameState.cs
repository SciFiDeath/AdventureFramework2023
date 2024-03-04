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

	public void SetVisibility(string name, bool value)
	{
		State[name] = value;
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

	public void AddVisibility(string name, bool value)
	{
		State.Add(name, value);
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

	public string Save(string key = "1234", string path = "gamestate.json")
	{
		string encrypted = "";

		try
		{
			encrypted = JsonUtility.EncryptGameStateInventory(State, ItemsInInventory, key);
			Console.WriteLine("Save successful");
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Error during save: {ex.Message}");
			// Log the exception or take appropriate actions
			throw new Exception("Could not encrypt and save", ex);
		}

		return encrypted;
	}


	public void LoadFromString(string encrypted)
	{

	}



}

