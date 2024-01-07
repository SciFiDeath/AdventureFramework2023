using Microsoft.AspNetCore.Components;
using Framework.Slides.JsonClasses;
using Framework.Game.Parameters;
using GameStateInventory;

namespace Framework.Slides;

public class SlideBase : ComponentBase
{

	[Parameter]
	public string SlideId { get; set; } = null!;

	// [Parameter]
	// public JsonSlide SlideData { get; set; } = null!; // Get the slide data
	protected JsonSlide SlideData { get; set; } = null!;


	[Parameter]
	public EventCallback<string> OnSlideChange { get; set; }

	[Inject]
	public GameState GameState { get; set; } = null!;

	[Inject]
	public SlideService SlideService { get; set; } = null!;

	// Fill color for the polygons
	//? what color will the polygons have (if any)
	protected string fillColor = "rgba(255, 255, 147, 0.5)";

	protected override void OnParametersSet()
	{
		SlideData = SlideService.GetSlide(SlideId);
		// Console.WriteLine(SlideId);
	}

	protected async Task SlideChange(string slideName)
	{
		await OnSlideChange.InvokeAsync(slideName);
	}

	protected async Task HandleButtonClick(JsonButton button)
	{
		if (button.Actions is not null)
		{
			await EvaluateActions(button.Actions);
		}
	}

	protected async Task EvaluateActions(List<List<string>> actions)
	{
		foreach (List<string> action in actions)
		{
			switch (action[0])
			{
				case "Route":
					await SlideChange(action[1]);
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