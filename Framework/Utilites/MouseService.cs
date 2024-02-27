using Microsoft.JSInterop;

namespace Framework.Mouse;

public interface IMouseService
{
	bool GetButtonState(int button);
	MouseState GetMouseState();
	MouseState GetStaticMouseState();
	event EventHandler<ClickEventArgs> OnMouseDown;
	event EventHandler<ClickEventArgs> OnMouseUp;
	Task SetDelay(int delay);
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

public class MouseService : IMouseService
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

	public event EventHandler<ClickEventArgs>? OnMouseDown;
	public event EventHandler<ClickEventArgs>? OnMouseUp;


	[JSInvokable]
	public void MouseUp(int button)
	{
		MouseState.Left = !(button == 0);
		MouseState.Right = !(button == 2);
		MouseState.Middle = !(button == 1);
		OnMouseUp?.Invoke(this, new ClickEventArgs { Button = button, Down = false });
	}

	[JSInvokable]
	public void MouseDown(int button)
	{
		MouseState.Left = button == 0;
		MouseState.Right = button == 2;
		MouseState.Middle = button == 1;
		OnMouseDown?.Invoke(this, new ClickEventArgs { Button = button, Down = true });
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

	public bool GetButtonState(int button)
	{
		return button switch
		{
			0 => MouseState.Left,
			1 => MouseState.Middle,
			2 => MouseState.Right,
			_ => false,
		};
	}

	public MouseState GetMouseState()
	{
		return MouseState;
	}

	public MouseState GetStaticMouseState()
	{
		return new MouseState
		{
			X = MouseState.X,
			Y = MouseState.Y,
			Left = MouseState.Left,
			Right = MouseState.Right,
			Middle = MouseState.Middle
		};
	}


}