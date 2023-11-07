using Microsoft.AspNetCore.Components;

namespace Framework.Slides;

public class SlideBase : ComponentBase
{
	[Parameter]
	public EventCallback<string> OnSlideChange { get; set; }
	
	protected async Task SlideChange(string slideName)
	{
		await OnSlideChange.InvokeAsync(slideName);
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