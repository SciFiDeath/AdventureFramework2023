using Framework.Slides;
using Microsoft.AspNetCore.Components;
using Framework.Game.Parameters;
using GameStateInventory;


namespace Framework.Game;

public partial class GameBase : ComponentBase
{
	[Inject]
	protected SlideService SlideService { get; set; } = null!;

	[Inject]
	protected GameState GameState { get; set; } = null!;

	// // private readonly TaskCompletionSource<bool> _tcs = new();
	// // protected Task InitTask => _tcs.Task;

	protected string SlideId => Parameters.SlideId;

	protected SlideComponentParameters Parameters { get; set; } = null!;
	// protected Dictionary<string, object?> ParametersDictionary { get; set; } = null!;


	protected override void OnInitialized()
	{
		string slideId = SlideService.GetStartSlideId();
		Parameters = new SlideComponentParameters()
		{
			SlideId = slideId,
			OnButtonClick = EventCallback.Factory.Create<List<List<string>>>(this, EvaluateActions)
		};
		// // _tcs.SetResult(true);
	}

	protected static void HandlePolygonClick(string thing)
	{
		Console.WriteLine(thing);
	}

	protected void ChangeSlide(string slideId)
	{
		Parameters.SlideId = slideId;
		// Console.WriteLine(SlideId);
		StateHasChanged();
	}

	protected void FinishMinigame(bool success)
	{
		if (success)
		{
			ChangeSlide(SlideService.GetSlide(Parameters.SlideId).FallbackSlide!);
		}
		// TODO: Also, maybe make this function actually do something different based on success
		else
		{
			ChangeSlide(SlideService.GetSlide(Parameters.SlideId).FallbackSlide!);
		}
	}

	protected async Task EvaluateActions(List<List<string>> actions)
	{
		foreach (List<string> action in actions)
		{
			switch (action[0])
			{
				case "Route":
					ChangeSlide(action[1]);
					break;
				case "Require":
					if (GameState.CheckForItem(action[1]))
					{
						GameState.RemoveItem(action[1]);
					}
					else
					{
						return;
					}
					break;
				default:
					break;
			}
		}
	}
}