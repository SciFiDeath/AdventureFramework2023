using Microsoft.AspNetCore.Components;
using GameStateInventory;
using FrameworkItems;
using JsonUtilities;
using static InventoryEvent;
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
        base.OnInitialized();
        // Subscribe to the ItemAdded event
        InventoryEvent.ItemAdded += HandleItemAdded;
        
        

        if (GameState == null)
        {
            // Log an error, throw an exception, or handle the null GameState case appropriately.
            Console.WriteLine("GameState is null!");
            return;
        }
        
        //GameState.AddItem("goldkey"); 
        GameState.AddItem("surfacecharger");    
        GameState.AddItem("frog");     
        GameState.AddItem("coffeemug");     
        InvItems = GameState.GetItemObjects();


    }

    private void HandleItemAdded(object sender, InventoryEvent.ItemAddedEventArgs e)
        {
            // Handle the item added event here
            // For example, update the UI, perform some action, etc.
            StateHasChanged(); // Update the UI if needed
        }
    
}