using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Rooms;

public partial class HM305 : ComponentBase
{

	private string nextRoom = "test";

	private string text = "This is test text";



	[Parameter]
	public EventCallback<string> OnRoomChange { get; set; }

	private async Task ChangeRoom()
	{
		await OnRoomChange.InvokeAsync(nextRoom);
	}

	[Parameter]
	public EventCallback<string> OnTextChange { get; set; }

	public async Task ChangeText()
	{
		await OnTextChange.InvokeAsync("New Text");
	}

	private void ShowText(string text)
	{
		Console.WriteLine(text);
	}
}