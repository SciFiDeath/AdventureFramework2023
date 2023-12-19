using Microsoft.AspNetCore.Components;
using GameStateInventory;
using FrameworkItems;
using JsonUtilities;
namespace Framework.InventoryUI;

public class InventoryUIBase : ComponentBase
{
    [Inject] 
    protected GameState? GameState { get; set; }

    [Inject]
    protected JsonUtility? JsonUtility { get; set; }

    [Inject]
    protected Items? Items { get; set; }
    
    public List<string> ItemNames = new();
    
    public List<string> InventoryItems = new();
    protected override async Task OnInitializedAsync()
    {
        if (GameState == null)
        {
            // Log an error, throw an exception, or handle the null GameState case appropriately.
            Console.WriteLine("GameState is null!");
            return;
        }
        await GameState.LoadGameStateAndItemsAsync();

        GameState.AddItem("goldkey");     
        ItemNames = GameState.GetItems();

    }
    public void AddItem()
    {
        GameState.AddItem("surfacecharger");
    }

}