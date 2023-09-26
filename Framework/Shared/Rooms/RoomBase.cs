using Microsoft.AspNetCore.Components;

namespace Room;

public class RoomBase : ComponentBase
{
	[Parameter]
	public EventCallback<string> OnRoomChange { get; set; }

	protected async Task ChangeRoom()
	{
		await OnRoomChange.InvokeAsync(); // add room routing stuff
	}

	public struct Button
	{
		string id;
		string points;
	}
}