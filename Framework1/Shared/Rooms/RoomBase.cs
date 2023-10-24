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

	public readonly struct Button
	{
		public readonly string Id;
		public readonly string Points;
		public readonly string Action;
		public readonly string[] Args;
		
		public Button(string Id, string Points, string Action, string[] Args)
		{
			this.Id = Id;
			this.Points = Points;
			this.Action = Action;
			this.Args = Args;
		}
		
		public Signal GetSignal()
		{
			return new Signal(Action, Args);
		}
	}
	
	public struct Signal 
	{
		public readonly string Action;
		public readonly string[] Args;
		
		public Signal(string Action, string[] Args)
		{
			this.Action = Action;
			this.Args = Args;
		}
	}
}