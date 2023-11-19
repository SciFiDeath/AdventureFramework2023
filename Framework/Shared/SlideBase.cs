using Microsoft.AspNetCore.Components;
using Framework.Slides.JsonClasses;
using Framework.Game.Parameters;

namespace Framework.Slides;

public class SlideBase : ComponentBase, ISlideComponentParameters
{
	[Parameter]
	public JsonSlide SlideData { get; set; } = null!; // Get the slide data 

	[Parameter]
	public EventCallback<string> OnSlideChange { get; set; }


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
				// case "Require":
				// 	if (ValidateRequire(action[1], action[2]))
				// 	{
				// 		return;
				// 	}
				// 	break;
				default:
					break;
			}
		}
	}
}