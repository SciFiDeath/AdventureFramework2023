using Framework.Slides.JsonClasses;
using Framework.Slides;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Framework.Game;

// could't get the injection of SlidesDeserializer to work, so I just copied the code
// from SlidesDeserializer into GameBase
// The deserialization part is in GameBaseDeserializer.cs, containing a partial class of GameBase
// I though everything in one file would be a bit messy, so I split it up
public partial class GameBase : ComponentBase
{
	protected Dictionary<string, JsonSlide> Slides {get; set; } = null!;
	
	protected override async Task OnInitializedAsync()
	{
		Slides = await GetSlides("Slides.json");
	}

	protected string SlideId = "HM305";
}