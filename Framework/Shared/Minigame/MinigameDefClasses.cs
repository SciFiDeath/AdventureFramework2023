using System.Numerics;
using Framework.Minigames;
using Framework.Minigames.MinigameDefClasses;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Framework.Minigames;


public class MiniTest : MinigameDefBase
{
	public override string BackgroundImage { get; set; } = "images/HM305_blackboard.jpg";

	public void Test(EventArgs e)
	{
		MouseEventArgs me = (MouseEventArgs)e;
		// Console.WriteLine($"X: {me.ClientX}, Y: {me.ClientY}");
		// Console.WriteLine("Test");
		Rect.X += 20;
		Console.WriteLine(Rect.X);
		// await Task.Delay(2000);
		Update();
		if (Rect.X > 300)
		{
			Finish(true);
		}

	}

	[Element] public Rectangle Rect { get; set; }

	public MiniTest()
	{
		Rect = new()
		{
			X = 100,
			Y = 100,
			Width = 100,
			Height = 100,
			ZIndex = 1,
			Fill = "red",
			OnClick = Test
		};
	}
}

public class CodeTerminal : MinigameDefBase
{
	public override string BackgroundImage { get; set; } = "images/calculator.png";

	public string Code { get; set; } = "1234";

	public string CurrentText { get; set; } = "";

	[Element] public Rectangle Button0 { get; set; }
	[Element] public Rectangle Button1 { get; set; }
	[Element] public Rectangle Button2 { get; set; }
	[Element] public Rectangle Button3 { get; set; }
	[Element] public Rectangle Button4 { get; set; }
	[Element] public Rectangle Button5 { get; set; }
	[Element] public Rectangle Button6 { get; set; }
	[Element] public Rectangle Button7 { get; set; }
	[Element] public Rectangle Button8 { get; set; }
	[Element] public Rectangle Button9 { get; set; }

	[Element] public Text Text { get; set; }

	[Element] public Image Key { get; set; }

	public bool Collected { get; set; } = false;

	public void SetNumber(int num)
	{
		if (CurrentText.Length < 4)
		{
			CurrentText += num.ToString();
			Text.InnerText = CurrentText;
			Update();
		}
		if (CurrentText.Length >= 4)
		{
			if (CurrentText == Code && Collected == false)
			{
				Key.Visible = true;
			}
			else
			{
				CurrentText = "";
			}
		}
	}

	public async void CollectKey(EventArgs e)
	{
		GameState.AddItem("goldkey");
		Key.Visible = false;
		GameState.ChangeVisibility("CodeTerminal");
		Collected = true;
		Update();
		await Task.Delay(2000);
		Finish(true);
	}

	public CodeTerminal()
	{
		int width = 72;
		int height = 42;

		string fill = "#ffff9380";

		Button7 = new()
		{
			X = 732,
			Y = 830,
			Width = width,
			Height = height,
			Fill = fill,
			OnClick = (args) => SetNumber(7),
		};
		Button8 = new()
		{
			X = 828,
			Y = 832,
			Width = width,
			Height = height,
			Fill = fill,
			OnClick = (args) => SetNumber(8),
		};
		Button9 = new()
		{
			X = 923,
			Y = 832,
			Width = width,
			Height = height,
			Fill = fill,
			OnClick = (args) => SetNumber(9),
		};
		Button4 = new()
		{
			X = 733,
			Y = 888,
			Width = width,
			Height = height,
			Fill = fill,
			OnClick = (args) => SetNumber(4),
		};
		Button5 = new()
		{
			X = 828,
			Y = 887,
			Width = width,
			Height = height,
			Fill = fill,
			OnClick = (args) => SetNumber(5),
		};
		Button6 = new()
		{
			X = 921,
			Y = 887,
			Width = width,
			Height = height,
			Fill = fill,
			OnClick = (args) => SetNumber(6),
		};
		Button1 = new()
		{
			X = 736,
			Y = 941,
			Width = width,
			Height = height,
			Fill = fill,
			OnClick = (args) => SetNumber(1),
		};
		Button2 = new()
		{
			X = 831,
			Y = 940,
			Width = width,
			Height = height,
			Fill = fill,
			OnClick = (args) => SetNumber(2),
		};
		Button3 = new()
		{
			X = 926,
			Y = 942,
			Width = width,
			Height = height,
			Fill = fill,
			OnClick = (args) => SetNumber(3),
		};
		Button0 = new()
		{
			X = 735,
			Y = 993,
			Width = width,
			Height = height,
			Fill = fill,
			OnClick = (args) => SetNumber(0),
		};
		Text = new()
		{
			InnerText = CurrentText,
			X = 620,
			Y = 490,
			FontSize = 100,
			Fill = "white"
		};
		Key = new()
		{
			X = 1400,
			Y = 230,
			Width = 450,
			OnClick = CollectKey,
			ImagePath = "InventoryImages/key2.png",
			Visible = false
		};
	}

}

public class KillTest : MinigameDefBase
{
	[Element]
	public Rectangle Generate { get; set; }

	[Element]
	public Rectangle Cancel { get; set; }

	public override string BackgroundImage { get; set; } = "images/HM3_hallwayE.jpg";

	public GameObjectContainer<Rectangle> Rects { get; } = new();

	public override async Task GameLoop(CancellationToken token)
	{
		while (true)
		{
			// if (token.IsCancellationRequested)
			// {
			// 	return;
			// }
			token.ThrowIfCancellationRequested();
			Rects.Transform((rect) => rect.Y += 5);
			Update();
			await Task.Delay(10, token);
		}

	}


	public Rectangle GenerateRect()
	{
		var rnd = new Random();
		int x = rnd.Next(100, 1900);
		int y = rnd.Next(100, 1000);

		string id = Guid.NewGuid().ToString("N");

		Rectangle r = new()
		{
			Id = id,
			X = x,
			Y = y,
			Width = 100,
			Height = 100,
			Fill = "red",
			OnClick = (args) => { Rects.KillId(id); Update(); }
		};

		return r;
	}

	public KillTest()
	{
		Generate = new()
		{
			X = 0,
			Y = 0,
			Width = 100,
			Height = 100,
			Fill = "blue",
			OnClick = (args) =>
			{
				var r = GenerateRect();
				Rects.Add(r);
				AddElement(r);
				Update();
			}
		};
		Cancel = new()
		{
			X = 0,
			Y = 100,
			Width = 100,
			Height = 100,
			Fill = "green",
			OnClick = (args) => { Cts.Cancel(); Console.WriteLine("Cancelled"); }
		};
	}

	public static void KillThing(SVGElement element)
	{
		element.Kill();
	}
}


public class LaurinsRain : MinigameDefBase
{
	// [Element] 
	// public Rectangle Rect {get; set;} = new(){ // With this method it is not possible to enable the OnClick event
	// 	X = 100,
	// 	Y = 100,
	// 	Width = 100,
	// 	Height = 100,
	// 	Fill = "red",	
	// 	};


	// [Element]
	// public Rectangle RectOnclick {get; set;}
	// public LaurinsRain(){ // With this, onclick is enabled => First make initialise all elements, then add properties in the constructor
	// 	RectOnclick = new(){
	// 		X = 200,
	// 		Y = 100,
	// 		Width = 100,
	// 		Height = 100,
	// 		Fill = "blue",
	// 		OnClick = (args) => {RectOnclick.Fill = "green"; Update();}
	// 	};
	// }

	public override string BackgroundImage { get; set; } = "images/HM3_hallwayN.jpg"; // Background Image
	public GameObjectContainer<Rectangle> Rects { get; } = new(); // Gameobject container that contains rectangles that are to be moved

	[Element]
	public Rectangle? Down { get; set; }

	[Element]
	public Rectangle? Quit { get; set; }

	public LaurinsRain()
	{
		Down = new()
		{ // Initialise button for spawning new rects
			X = 0,
			Y = 0,
			Width = 100,
			Height = 100,
			Fill = "green",
			OnClick = (args) => Rects.Transform((r) => { r.Y += 30; Update(); }) // When clicked, every rectangle in the container moves down by 30 pixels
		};
		Quit = new()
		{ // Initialise button for quitting the game
			X = 0,
			Y = 100,
			Width = 100,
			Height = 100,
			Fill = "red",
			OnClick = (args) => Finish(true)
		};

		for (var i = 100; i <= 1920; i += 100)
		{ // Initialise row of elements at once
			var Rect = new Rectangle()
			{
				X = i + 20,
				Y = 0,
				Width = 50,
				Height = 50,
				Fill = "blue",
			};
			Rects.Add(Rect);
			AddElement(Rect);
		}
	}



}

public class ElementStyleTest : MinigameDefBase
{
	public override string BackgroundImage { get; set; } = "images/HM3_hallwayW.jpg";

	[Element]
	public Rectangle Rectangle { get; set; } = new()
	{
		X = 100,
		Y = 100,
		Width = 200,
		Height = 100,
		Fill = "red",
		CustomStyle = "fill: blue",
		OnClick = (args) => Console.WriteLine("click"),
		OnDoubleClick = (args) => Console.WriteLine("doubleclick"),
		OnMouseEnter = (args) => Console.WriteLine("enter"),
		OnMouseLeave = (args) => Console.WriteLine("leave"),
	};

	[Element]
	public Image Image { get; set; } = new()
	{
		ImagePath = "InventoryImages/SurfaceCharger.jpeg",
		X = 100,
		Y = 400,
		Width = 300,
		// Height = 100,
		Opacity = 0.9,
	};

	[Element]
	public Text Text { get; set; } = new()
	{
		InnerText = "Hello World",
		X = 500,
		Y = 500,
		FontSize = 100,
		FontFamily = "Comic Sans MS",
		TextLength = 1000,
		// StretchLetters = true,
		// Selectable = true,
	};

	[Element]
	public Polygon Polygon { get; set; } = new()
	{
		Points = [[500, 500], [1000, 500], [800, 700], [650, 600]],
		Fill = "white",
		Opacity = 0.5,
	};

	[Element]
	public Polyline Polyline { get; set; } = new()
	{
		Points = [[1000, 500], [1500, 500], [1300, 700], [1150, 600]],
		Stroke = "blue",
		StrokeWidth = 20,
		Opacity = 0.5,
		Fill = "none"
	};

	[Element]
	public Line Line { get; set; } = new()
	{
		X1 = 1500,
		X2 = 1700,
		Y1 = 0,
		Y2 = 1000,
		Stroke = "aquamarine",
		StrokeWidth = 400,
		StrokeOpacity = 0.2,
	};

	[Element]
	public Circle Circle { get; set; } = new()
	{
		CX = 2000,
		CY = 0,
		R = 750,
		Fill = "darkviolet",
		FillOpacity = 0.3,
		Stroke = "black",
		StrokeWidth = 20,

	};

	[Element]
	public Ellipse Ellipse { get; set; } = new()
	{
		CX = 1000,
		CY = 750,
		RX = 400,
		RY = 100,
		Fill = "goldenrod",
		FillOpacity = 0.7,
		Cursor = "pointer"
	};
}

public class ElementTest : MinigameDefBase
{

	public override string BackgroundImage { get; set; } = "images/HM3_hallwayN.jpg";

	[Element]
	public RawMarkup Markup { get; set; }

	[Element]
	public ForeignObject ForeignTest { get; set; }

	public CustomObject CustomTest { get; set; } = new()
	{
		CustomTagName = "div",
		Content = ["Lorem ipsum dolor, sit amet consectetur adipisicing elit. Quisquam, sunt at aperiam dolore voluptatem quidem itaque sed id tenetur praesentium, minus error rerum? Nobis excepturi nostrum explicabo? Fugit, neque voluptatibus!", new CustomObject() { CustomTagName = "h1", Content = ["hihihi"] }],
		Callbacks = new()
		{
			{"onclick", (e) => Console.WriteLine("hellooooojoij")}
		},
	};

	[Element]
	public Text Text { get; set; }

	public ElementTest()
	{
		Text = new()
		{
			FontFamily = "Comic Sans MS",
			ContentMode = true,
			X = 10,
			Y = 50,
			Content =
			[
				new Tspan() {InnerText = "muha", Fill = "red", OnClick = (e) => Console.WriteLine("hello") },
				"\n\noh noes"
			]
		};

		Markup = new()
		{
			Markup = @"
			<foreignObject x=""20"" y=""20"" width=""160"" height=""160"">
				<div xmlns=""http://www.w3.org/1999/xhtml"">
				Lorem ipsum dolor sit amet,
				consectetur adipiscing elit.
				Sed mollis mollis mi ut ultricies.Nullam magna ipsum,
				porta vel dui convallis,
				rutrum imperdiet eros.
				Aliquam erat volutpat.
				</div>
			</foreignObject>
			"
		};

		ForeignTest = new()
		{
			X = 500,
			Y = 50,
			Width = 500,
			Height = 500,
			CustomObject = CustomTest
		};
	}

}