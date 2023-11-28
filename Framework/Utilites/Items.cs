using JsonUtilities; // For Fetching Json. Async Functions
using Microsoft.AspNetCore.Components; // For Injecting.

namespace Items
{

public class Item
{
    //* Declare Item Object to use as Type in Json file loading *//
    public string Name { get; }
    public string Description { get; }
    public string Image { get; }

    public Item(string name, string description, string image)
    {
        Name = name;
        Description = description;
        Image = image;
    }
}

public class Items
{   
    protected JsonUtility JsonUtility { get; set; } = null!;
    
    public Items(JsonUtility jsonUtility)
    {
        JsonUtility = jsonUtility;
    }
    public static Dictionary<string, Item> items = new();

    
    public async Task LoadItemsAsync(string path = "items.json")
    {
        //* Load Items from items.json *// TODO needs error checking
        
        items = await JsonUtility.LoadFromJsonAsync<Dictionary<string, Item>>(path);
    }
    public Item GetPropertiesByName(string ItemName) 
    {
        //*Returns Item Object With: Name, Desc. and Image Path*// 

        // Make sure items.json has been read
        if (items.Count == 0)
        {
            throw new Exception("No items read, Call LoadItemsAsync() first.");
        }
        
        Console.WriteLine(items[ItemName].Description);
        Console.WriteLine(items[ItemName].Image);
        return items[ItemName];

    }

    public bool DoesItemExist(string ItemName)
    {
        return items.ContainsKey(ItemName);
    }

}

}