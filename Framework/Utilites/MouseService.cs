using Microsoft.JSInterop;

namespace Framework.Mouse;

public struct MouseState
{
	public int X;
	public int Y;
	public bool Left;
	public bool Right;
	public bool Middle;
}

public class MouseService
{
	private readonly IJSRuntime jsRuntime;

	public MouseService(IJSRuntime jsRuntime)
	{
		this.jsRuntime = jsRuntime;
	}

	private DotNetObjectReference<MouseService> objRef = null!;

	public async Task Init(bool disableContextMenu = true)
	{
		objRef = DotNetObjectReference.Create(this);
		await jsRuntime.InvokeVoidAsync("mouse.init", objRef, disableContextMenu);
	}

	public MouseState MouseState = new()
	{
		X = 0,
		Y = 0,
		Left = false,
		Right = false,
		Middle = false
	};


	[JSInvokable]
	public void MouseUp(int button)
	{
		MouseState.Left = !(button == 0);
		MouseState.Right = !(button == 2);
		MouseState.Middle = !(button == 1);
	}

	[JSInvokable]
	public void MouseDown(int button)
	{
		MouseState.Left = button == 0;
		MouseState.Right = button == 2;
		MouseState.Middle = button == 1;
	}

	[JSInvokable]
	public void MouseMove(int x, int y)
	{
		MouseState.X = x;
		MouseState.Y = y;
	}

	public async Task SetDelay(int delay)
	{
		await jsRuntime.InvokeVoidAsync("mouse.setDelay", delay);
	}
}