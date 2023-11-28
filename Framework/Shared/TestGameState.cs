using Microsoft.AspNetCore.Components;
using GameStateInventory;
using Framework.wwwroot;
namespace Framework.TestGameState;

public class TestGameStateBase : ComponentBase
{
    [Inject]
    protected GameState GameState { get; set;} = null!;

    private Dictionary<string, bool> State = new();

    public bool? Button;
    protected override async Task OnInitializedAsync()
    {
        await GameState.LoadGameStateAsync();

        GameState.AddItem("surfacecharger");

        Button = GameState.CheckForItem("surfacecharger");

    }
}