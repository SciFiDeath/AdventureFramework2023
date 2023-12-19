using Framework.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Reflection;
using GameStateInventory;

namespace Framework.Minigames;

/*
Current Plan:

MinigameBase(ComponentBase):
	The base class for the Minigame Razor Component
	Two approaches for passing the data:
		Pass a string of the class which is the MGD for the Minigame, 
		then use reflection to create an instance of the class and use it
		Or create the instance on the Game-Level and pass it to the MG
		-> I would say it doesn't really matter, but the first one hides away
		the data from the Game-Level, which is kinda nice
	The MGB will render in the different elements contained in the MGD
	It also contains an EventCallback for when the Minigame is finished
	This Callback returns a bool, when it's false, nothing happens and the 
	player gets routed to the previous slide, when it's true, the Game 
	executes the actions specified in the json file

MinigameDefBase:
	The base class for the Minigame Definition
	This is the class that will be subclassed by the others to create a minigame
	It contains all the data and logic that is needed for the minigame
	
SvgElement and Subclasses:
	A class that represents an SVG Element
	Contains style and other attributes
	Some subclasses will have additional methods to make changing state a bit easier

Slides.json things:
	I will have to add the possibility to make slides of type Minigame
	They will have actions, that can be executed just like the actions of slide buttons. 
	With this level of polymorphism, I just have to make every property in the 
	JsonClasses nullable and then make my own checks, as there are certain conditions
	to when null is fine and when not, more complicated than the compiler can check.
*/

public class MinigameBase : ComponentBase
{
	// Inject the GameState
	[Inject]
	public GameState GameState { get; set; } = null!;

	[Parameter]
	public string MinigameDefClass { get; set; } = null!;

	[Parameter]
	public EventCallback<bool> OnFinished { get; set; }

	protected async Task Finish(bool success)
	{
		await OnFinished.InvokeAsync(success);
	}

	protected MinigameDefBase MinigameDef { get; set; } = null!;

	protected override void OnInitialized()
	{
		// Get the type from the string classname
		var type = Type.GetType(MinigameDefClass) ??
		// if the type is not found, thow an exception
		throw new Exception($"No class \"{MinigameDefClass}\" found");

		try
		{
			// create instance of the type
			var instance = Activator.CreateInstance(type) ??
			// if the instance is null, throw an exception
			throw new Exception($"Could not create instance of \"{MinigameDefClass}\"");

			// cast the instance
			MinigameDef = (MinigameDefBase)instance;
			// attach events
			MinigameDef.Finished += async (sender, e) => await Finish(e.Success);
			MinigameDef.UpdateEvent += (sender, e) => StateHasChanged();
			// attach gamestate
			MinigameDef.GameState = GameState;
			// Run the AfterInit method
			MinigameDef.AfterInit();

		}
		catch (Exception e)
		{
			// if there is an exception, throw a new one with the original exception as inner exception
			throw new Exception($"Error while creating instance of object \"{MinigameDefClass}\"", e);
		}
	}
}

public abstract class MinigameDefBase
{
	public List<SVGElement> Elements { get; set; } = new();

	public abstract string BackgroundImage { get; set; }

	public GameState GameState { get; set; } = null!;

	public void Init()
	{
		// create a list with all the elements in the Minigame
		// so that we don't have to use reflection every time
		// the thing renders
		// loop over properties as test
		// foreach (var property in GetType().GetProperties())
		// {
		// 	// Console.WriteLine(property.Name);
		// 	// Console.WriteLine(property.GetValue(this));
		// }
		foreach (var property in GetType().GetProperties())
		{
			if (Attribute.IsDefined(property, typeof(ElementAttribute)))
			{
				// check if the property is a SVGElement if it is, 
				// cast and assign it to element, then add it to the list
				if (property.GetValue(this) is SVGElement element)
				{
					Elements.Add(element);
				}
			}
		}
		// sort the list by ZIndex so that higher ZIndex elements appear first
		Elements.Sort((b, a) => a.ZIndex.CompareTo(b.ZIndex));
		// Console.WriteLine(Elements.Count);
	}

	// Method that is run right after the constructor
	public virtual void AfterInit() { }


	// Event handlers
	public event EventHandler<FinishedEventArgs>? Finished;
	public event EventHandler? UpdateEvent;

	public void Finish(bool success)
	{
		Finished?.Invoke(this, new FinishedEventArgs { Success = success });
	}

	// Btw, I think I found out why it worked before without this:
	// I think it is because whenever I clicked the box, an event callback was triggered,
	// thus notifying the game that something happened and it should rerender
	public void Update()
	{
		UpdateEvent?.Invoke(this, EventArgs.Empty);
	}

}

public class FinishedEventArgs : EventArgs
{
	public bool Success { get; set; }
}


public abstract class NamedAttribute : Attribute
{
	public readonly string? name;
	public NamedAttribute(string? name = null)
	{
		this.name = name;
	}
}

[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
public sealed class StyleAttribute : NamedAttribute
{
	public StyleAttribute(string? name = null) : base(name) { }
}

[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
public sealed class HtmlAttribute : NamedAttribute
{
	public HtmlAttribute(string? name = null) : base(name) { }
}

[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
public sealed class CallbackAttribute : NamedAttribute
{
	public CallbackAttribute(string? name = null) : base(name) { }
}

[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
public sealed class ElementAttribute : Attribute { }

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public sealed class ElementNameAttribute : NamedAttribute
{
	public ElementNameAttribute(string? name = null) : base(name) { }
}


public abstract class SVGElement
{
	[Html] public string? Style { get => GetStyleString(); }

	public int ZIndex { get; set; } = 0;

	// // Implementation of the TagName property as static
	// public static string TagName { get; } = "svg";

	// Normal implementation (maybe slightly slower, but I understand it better)
	public abstract string TagName { get; }

	public virtual RenderFragment GetRenderFragment()
	{
		return builder =>
		{
			builder.OpenElement(0, TagName);
			builder.AddMultipleAttributes(1, GetElementAttributeDictionary());
			builder.AddAttribute(2, "style", Style);
			builder.AddMultipleAttributes(3, GetCallbackDictionary());
			builder.CloseElement();
		};
	}

	// [Obsolete("Strings cannot contain blazor stuff, use RenderFragments instead")]
	// public abstract string GetMarkupString();

	// maybe problems with reactivity, don't know yet
	private string? GetStyleStringOld()
	{
		string style = "";
		foreach (var property in GetType().GetProperties())
		{
			if (Attribute.IsDefined(property, typeof(StyleAttribute)))
			{
				var value = property.GetValue(this);
				if (value != null)
				{
					style += $"{Translate(property.Name)}: {property.GetValue(this)};";
				}
			}
		}
		return style == "" ? null : style;
	}

	public string? GetStyleString()
	{
		string style = "";
		PropertyInfo[] properties = GetType().GetProperties()
		.Where(p => Attribute.IsDefined(p, typeof(StyleAttribute)))
		.ToArray();
		foreach (var property in properties)
		{
			var value = property.GetValue(this);
			if (value != null)
			{
				//*Note: Added null suppression because safety is ensured, but keep in mind regardless
				style += $"{property.GetCustomAttribute<StyleAttribute>()!.name ?? Translate(property.Name)}: {property.GetValue(this)};";
			}
		}
		return style == "" ? null : style;
	}

	[Obsolete("Strings cannot contain blazor stuff, use RenderFragments instead")]
	public string? GetElementAttributeString()
	{
		string attributes = "";
		foreach (var property in this.GetType().GetProperties())
		{
			if (Attribute.IsDefined(property, typeof(HtmlAttribute)))
			{
				var value = property.GetValue(this);
				if (value != null)
				{
					attributes += $"{Translate(property.Name)}=\"{property.GetValue(this)}\" ";
				}
			}
		}
		return attributes == "" ? null : attributes;
	}

	[Obsolete("Strings cannot contain blazor stuff, use RenderFragments instead")]
	public string? GetCallbackString()
	{
		string callbacks = "";
		foreach (var method in this.GetType().GetMethods())
		{
			if (Attribute.IsDefined(method, typeof(CallbackAttribute)))
			{
				callbacks += $"{Translate(method.Name)}=\"{method.Name}\" ";
			}
		}
		return callbacks == "" ? null : callbacks;
	}

	public Dictionary<string, object>? GetElementAttributeDictionary()
	{
		Dictionary<string, object> attributes = new();
		PropertyInfo[] properties = GetType().GetProperties()
		.Where(p => Attribute.IsDefined(p, typeof(HtmlAttribute)))
		.ToArray();
		foreach (var property in properties)
		{
			var value = property.GetValue(this);
			if (value != null)
			{
				attributes.Add(
					//*Note: Added null suppression because safety is ensured, but keep in mind regardless
					property.GetCustomAttribute<HtmlAttribute>()!.name ?? Translate(property.Name),
					value
				);
			}
		}
		// Console.WriteLine(attributes.Count);
		return attributes.Count == 0 ? null : attributes;
	}

	public Dictionary<string, object>? GetCallbackDictionary()
	{
		Dictionary<string, object> callbacks = new();
		PropertyInfo[] methods = GetType().GetProperties()
		.Where(p => Attribute.IsDefined(p, typeof(CallbackAttribute)))
		.ToArray();
		foreach (var method in methods)
		{
			// callbacks.Add(
			// 	Translate(method.Name),
			// 	EventCallback.Factory.Create(this, (Action)Delegate.CreateDelegate(typeof(EventHandler), this, method))
			// );
			if (method.GetValue(this) is Action<EventArgs> action)
			{
				callbacks.Add(
					//*Note: I added null suppresion here, because it is impossible that the attribute is null
					// But still, I could be overlooking somehting, so be wary of this
					// If this throws an error, we're in trouble, as it means that something greater
					// beyond my mortal understanding has broken
					method.GetCustomAttribute<CallbackAttribute>()!.name ?? Translate(method.Name),
					EventCallback.Factory.Create(this, action)
				);
			}
		}
		return callbacks.Count == 0 ? null : callbacks;
	}



	// public Dictionary<string, object>? GetElementAttributeDictionary()
	// {
	// 	Dictionary<string, object> attributes = new();
	// 	foreach (var property in GetType().GetProperties())
	// 	{
	// 		if (Attribute.IsDefined(property, typeof(HtmlAttribute)))
	// 		{
	// 			var value = property.GetValue(this);
	// 			if (value != null)
	// 			{
	// 				attributes.Add(Translate(property.Name), value);
	// 			}
	// 		}
	// 	}
	// 	return attributes.Count == 0 ? null : attributes;
	// }



	protected static string ConvertCamelToKebab(string camelCase)
	{
		string kebab = $"{char.ToLower(camelCase[0])}";
		foreach (char c in camelCase[1..])
		{
			if (char.IsUpper(c))
			{
				kebab += $"-{char.ToLower(c)}";
			}
			else
			{
				kebab += c;
			}
		}
		return kebab;
	}

	// TODO: fix the usage and implementation of this method
	public static string Translate(string key)
	{
		// for EventHandlers
		// everything that starts with an "On" and has a capital letter after that
		// will be translated to "on{lowercase}" with an @ in front, because blazor
		// e.g. OnClick -> @onclick
		// So just make sure that you name everything correctly or else everything will break
		// Update: I don't think the callbacks work with strings, but still working on it
		try
		{
			if (key[..2] == "On" && char.IsUpper(key[2]))
			{
				return $"on{key[2..].ToLower()}";
			}
		}
		catch (ArgumentOutOfRangeException)
		{
			// if the string is too short, use the default translation
			return ConvertCamelToKebab(key);
		}
		return ConvertCamelToKebab(key);
	}

}

// Some classes like ImgButton, PolygonButton, RectButton are some Ideas I had
// They are set as properties in a  MinigameBase instance and are then
// "generated" in the markup of the Minigame
// Should have functions like check for click, disable/enable, Set/GetPos, show/hide et.

public class Rectangle : SVGElement
{
	// // Implementation of the TagName property as static
	// public new static string TagName { get; } = "rect";

	// "normal" implementation of TagName
	public override string TagName { get; } = "rect";

	[Html] public string? Id { get; set; }

	[Html] public int? X { get; set; }
	[Html] public int? Y { get; set; }
	[Html] public int Width { get; set; }
	[Html] public int Height { get; set; }

	[Html] public string? Fill { get; set; }

	[Callback] public Delegate? OnClick { get; set; }

	// public override RenderFragment GetRenderFragment()
	// {
	// 	return builder =>
	// 	{
	// 		builder.OpenElement(0, "rect");
	// 		builder.AddMultipleAttributes(1, GetElementAttributeDictionary());
	// 		builder.AddAttribute(2, "style", Style);
	// 		builder.AddMultipleAttributes(3, GetCallbackDictionary());
	// 		builder.CloseElement();
	// 	};
	// }
}

public class Text : SVGElement
{
	public override string TagName { get; } = "text";

	public string? InnerText { get; set; }

	[Html] public int? X { get; set; }
	[Html] public int? Y { get; set; }
	[Html] public string? Fill { get; set; }
	[Style("font-size")] public string? FontSize { get; set; }
	[Style("font-family")] public string? FontFamily { get; set; }


	public override RenderFragment GetRenderFragment()
	{
		return builder =>
		{
			builder.OpenElement(0, TagName);
			builder.AddMultipleAttributes(1, GetElementAttributeDictionary());
			builder.AddAttribute(2, "style", Style);
			builder.AddMultipleAttributes(3, GetCallbackDictionary());
			builder.AddContent(4, InnerText);
			builder.CloseElement();
		};
	}
}

public class SVGImage : SVGElement
{
	public override string TagName { get; } = "image";

	[Html] public int? X { get; set; }
	[Html] public int? Y { get; set; }
	[Html] public int? Width { get; set; }
	[Html] public int? Height { get; set; }

	[Style("display")] public string? Visibility { get; set; }

	[Html("href")] public string? Image { get; set; }

	[Callback] public Delegate? OnClick { get; set; }

}


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
		Init();
	}
}

public class CodeTerminal : MinigameDefBase
{
	public override string BackgroundImage { get; set; } = "images/calculator.png";

	public string Code { get; set; } = "0385";

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

	[Element] public SVGImage Key { get; set; }

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
				Key.Visibility = "block";
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
		Console.WriteLine("test");
		Key.Visibility = "none";
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
			OnClick = (EventArgs args) => SetNumber(7),
		};
		Button8 = new()
		{
			X = 828,
			Y = 832,
			Width = width,
			Height = height,
			Fill = fill,
			OnClick = (EventArgs args) => SetNumber(8),
		};
		Button9 = new()
		{
			X = 923,
			Y = 832,
			Width = width,
			Height = height,
			Fill = fill,
			OnClick = (EventArgs args) => SetNumber(9),
		};
		Button4 = new()
		{
			X = 733,
			Y = 888,
			Width = width,
			Height = height,
			Fill = fill,
			OnClick = (EventArgs args) => SetNumber(4),
		};
		Button5 = new()
		{
			X = 828,
			Y = 887,
			Width = width,
			Height = height,
			Fill = fill,
			OnClick = (EventArgs args) => SetNumber(5),
		};
		Button6 = new()
		{
			X = 921,
			Y = 887,
			Width = width,
			Height = height,
			Fill = fill,
			OnClick = (EventArgs args) => SetNumber(6),
		};
		Button1 = new()
		{
			X = 736,
			Y = 941,
			Width = width,
			Height = height,
			Fill = fill,
			OnClick = (EventArgs args) => SetNumber(1),
		};
		Button2 = new()
		{
			X = 831,
			Y = 940,
			Width = width,
			Height = height,
			Fill = fill,
			OnClick = (EventArgs args) => SetNumber(2),
		};
		Button3 = new()
		{
			X = 926,
			Y = 942,
			Width = width,
			Height = height,
			Fill = fill,
			OnClick = (EventArgs args) => SetNumber(3),
		};
		Button0 = new()
		{
			X = 735,
			Y = 993,
			Width = width,
			Height = height,
			Fill = fill,
			OnClick = (EventArgs args) => SetNumber(0),
		};
		Text = new()
		{
			InnerText = CurrentText,
			X = 620,
			Y = 490,
			FontSize = "100px",
			Fill = "white"
		};
		Key = new()
		{
			X = 1400,
			Y = 230,
			Width = 450,
			OnClick = CollectKey,
			Image = "InventoryImages/key2.png",
			Visibility = "none"
		};

		Init();
	}

}