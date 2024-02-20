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
    
    public Dictionary<string, Item> InvItems = new();
    protected override async Task OnInitializedAsync()
    {
        if (GameState == null)
        {
            // Log an error, throw an exception, or handle the null GameState case appropriately.
            Console.WriteLine("GameState is null!");
            return;
        }
        
        GameState.AddItem("goldkey"); 
        GameState.AddItem("surfacecharger");    
        GameState.AddItem("frog");     
        GameState.AddItem("coffeemug");     
        InvItems = GameState.GetItemObjects();


    }

}