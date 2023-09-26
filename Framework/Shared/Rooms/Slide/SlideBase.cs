using Microsoft.AspNetCore.Components;

namespace Room.Slide;

public class SlideBase : ComponentBase
{
	[Parameter]
	public EventCallback<string[]> OnSlideChange { get; set; }

	protected async Task ChangeSlide()
	{
		await OnSlideChange.InvokeAsync(/*slide name goes here*/);
	}
}