using Microsoft.JSInterop;

namespace Framework.Mouse;

public interface IMouseService
{
	bool GetButtonState(int button);
	MouseState MouseState { get; }
	// MouseState GetStaticMouseState();
	event EventHandler<ClickEventArgs> OnMouseDown;
	event EventHandler<ClickEventArgs> OnMouseUp;
	Task SetDelay(int delay);
	Task<MouseState> GetMouseStateAsync();
}

public class ClickEventArgs : EventArgs
{
	public int Button;
	public bool Down;
}

public struct MouseState
{
	public int X;
	public int Y;
	public bool Left;
	public bool Right;
	public bool Middle;
}

public class MouseService(IJSRuntime jsRuntime) : IMouseService
{
	private readonly IJSRuntime jsRuntime = jsRuntime;
	private DotNetObjectReference<MouseService> objRef = null!;

	public async Task Init(bool disableContextMenu = true)
	{
		objRef = DotNetObjectReference.Create(this);
		await jsRuntime.InvokeVoidAsync("mouse.init", objRef, disableContextMenu);
	}

	private MouseState mouseState = new()
	{
		X = 0,
		Y = 0,
		Left = false,
		Right = false,
		Middle = false
	};

	public event EventHandler<ClickEventArgs>? OnMouseDown;
	public event EventHandler<ClickEventArgs>? OnMouseUp;


	[JSInvokable]
	public void MouseUp(int button)
	{
		mouseState.Left = !(button == 0);
		mouseState.Right = !(button == 2);
		mouseState.Middle = !(button == 1);
		OnMouseUp?.Invoke(this, new ClickEventArgs { Button = button, Down = false });
	}

	[JSInvokable]
	public void MouseDown(int button)
	{
		mouseState.Left = button == 0;
		mouseState.Right = button == 2;
		mouseState.Middle = button == 1;
		OnMouseDown?.Invoke(this, new ClickEventArgs { Button = button, Down = true });
	}

	[JSInvokable]
	public void MouseMove(int x, int y)
	{
		mouseState.X = x;
		mouseState.Y = y;
	}

	public async Task SetDelay(int delay)
	{
		await jsRuntime.InvokeVoidAsync("mouse.setDelay", delay);
	}

	public bool GetButtonState(int button)
	{
		return button switch
		{
			0 => mouseState.Left,
			1 => mouseState.Middle,
			2 => mouseState.Right,
			_ => false,
		};
	}

	// get the actual mouse state
	public MouseState MouseState => mouseState;

	// get a copy of the mouse state
	// public MouseState GetStaticMouseState()
	// {
	// 	return new MouseState
	// 	{
	// 		X = MouseState.X,
	// 		Y = MouseState.Y,
	// 		Left = MouseState.Left,
	// 		Right = MouseState.Right,
	// 		Middle = MouseState.Middle
	// 	};
	// }

	// get the most up to date mouse state no matter what
	public async Task<MouseState> GetMouseStateAsync()
	{
		Dictionary<string, int> state = new(await jsRuntime.InvokeAsync<Dictionary<string, int>>("mouse.getSvgMousePos"));
		return new MouseState
		{
			X = state["x"],
			Y = state["y"],
			Left = mouseState.Left,
			Right = mouseState.Right,
			Middle = mouseState.Middle
		};
	}
}


