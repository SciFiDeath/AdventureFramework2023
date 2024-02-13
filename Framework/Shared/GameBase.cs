using Framework.Slides.JsonClasses;
using Framework.Slides;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Framework.Game.Parameters;
using GameStateInventory;


namespace Framework.Game;

public partial class GameBase : ComponentBase
{
	[Inject]
	protected SlideService SlideService { get; set; } = null!;

	// Stuff for easier debugging
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
			OnSlideChange = EventCallback.Factory.Create<string>(this, ChangeSlide)
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
}