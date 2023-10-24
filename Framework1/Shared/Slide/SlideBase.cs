using Microsoft.AspNetCore.Components;
using Room;

namespace Room.Slide;

public class SlideBase : ComponentBase
{
	[Parameter]
	public EventCallback<Room.RoomBase.Signal> OnSlideSignal { get; set; }

	protected async Task ChangeSlide()
	{
		await OnSlideSignal.InvokeAsync(/*slide name goes here*/);
	}
}