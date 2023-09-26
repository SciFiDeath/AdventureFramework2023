using Microsoft.AspNetCore.Components;

namespace Rooms;

public class RoomBase : ComponentBase
{
	[Parameter]
	public EventCallback<string> OnRoomChange { get; set; }

	public async Task ChangeRoom(string nextRoom)
	{
		await OnRoomChange.InvokeAsync(nextRoom);
	}

}