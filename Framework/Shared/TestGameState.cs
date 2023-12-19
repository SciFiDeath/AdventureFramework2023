using Microsoft.AspNetCore.Components;
using GameStateInventory;
using FrameworkItems;
using Framework.wwwroot;
using JsonUtilities;
namespace Framework.TestGameState;

public class TestGameStateBase : ComponentBase
{
    [Inject]
    protected GameState? GameState { get; set; }

    [Inject]
    protected JsonUtility? JsonUtility { get; set; }

    [Inject]
    protected Items? Items { get; set; }
    public bool? Button;
    public bool? lebutton;


    public List<string> it = new();

    protected override async Task OnInitializedAsync()
    {
        if (GameState == null)
        {
            // Log an error, throw an exception, or handle the null GameState case appropriately.
            Console.WriteLine("GameState is null!");
            return;
        }
        await GameState.LoadGameStateAndItemsAsync();
        Console.WriteLine("GameState used once successfully");

        await Items.LoadItemsAsync();
        
        Button = GameState.CheckForItem("surfacecharger");

        Console.WriteLine(Button);
        
        GameState.AddItem("surfacecharger");

        Button = GameState.CheckForItem("surfacecharger");

        it = GameState.GetItems();

        lebutton = GameState.CheckVisibility("LeButton");
        GameState.ChangeVisibility("LeButton");
        Console.WriteLine("New LeBUtton");
        Console.WriteLine(GameState.CheckVisibility("LeButton"));
        




    }
}
