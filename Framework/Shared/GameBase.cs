using Framework.Slides.JsonClasses;
using Framework.Slides;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Framework.Game.Parameters;


namespace Framework.Game;

// could't get the injection of SlidesDeserializer to work, so I just copied the code
// from SlidesDeserializer into GameBase
// The deserialization part is in GameBaseDeserializer.cs, containing a partial class of GameBase
// I though everything in one file would be a bit messy, so I split it up
public partial class GameBase : ComponentBase
{
	[Inject]
	protected SlideService SlideService { get; set; } = null!;

	protected string SlideId => Parameters.SlideId;

	protected SlideComponentParameters Parameters { get; set; } = null!;
	// protected Dictionary<string, object?> ParametersDictionary { get; set; } = null!;


	protected override async Task OnInitializedAsync()
	{
		// TODO: Make it so that this runs at initialization of entire thing
		await SlideService.Init();
		string slideId = SlideService.GetStartSlideId();
		Parameters = new SlideComponentParameters()
		{
			SlideId = slideId,
			OnSlideChange = EventCallback.Factory.Create<string>(this, ChangeSlide)
		};
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