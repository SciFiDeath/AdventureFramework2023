using Framework.Slides.JsonClasses;
using Framework.Slides;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Framework.Game.Parameters;

using System.Diagnostics.CodeAnalysis;

namespace Framework.Game;

// could't get the injection of SlidesDeserializer to work, so I just copied the code
// from SlidesDeserializer into GameBase
// The deserialization part is in GameBaseDeserializer.cs, containing a partial class of GameBase
// I though everything in one file would be a bit messy, so I split it up
public partial class GameBase : ComponentBase
{
	protected Dictionary<string, JsonSlide> Slides {get; set; } = null!;
	
	protected string SlideId { get; set; } = null!;
	protected SlideComponentParameters Parameters { get; set; } = null!;
	// protected Dictionary<string, object?> ParametersDictionary { get; set; } = null!;
	
	
	protected override async Task OnInitializedAsync()
	{
		Slides = await GetSlidesUnsafe("Slides.json");
		SlideId = Slides.Keys.First();
		Parameters = new SlideComponentParameters() 
		{
			SlideData = Slides[SlideId],
			OnSlideChange = EventCallback.Factory.Create<string>(this, ChangeSlide)
		};
	}
	
	protected static void HandlePolygonClick(string thing) {
		Console.WriteLine(thing);
	}

	protected void ChangeSlide(string slideId) {
		Parameters.SlideData = Slides[slideId];
		StateHasChanged();
	}


	
}