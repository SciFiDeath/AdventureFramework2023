using Framework.Shared;
using Microsoft.AspNetCore.Components;
using System;
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



public abstract class MinigameBase : ComponentBase
{
	protected List<SVGElement> Elements { get; set; } = new();
	
	public abstract string BackgroundImage { get; set; }
	
	protected override void OnInitialized()
	{
		// create a list with all the elements in the Minigame
		// so that we don't have to use reflection every time
		// the thing renders
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
	}
}



[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
public sealed class StyleAttribute : Attribute {}

[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
public sealed class HtmlAttribute : Attribute {}

[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
public sealed class ElementAttribute : Attribute {}

[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
public sealed class CallbackAttribute : Attribute {}


public abstract class SVGElement
{
	[Html] public string? Style { get => GetStyle(); }
	
	public int ZIndex { get; set; } = 0;
	
	public string? PName { get; set; }
	
	public abstract RenderFragment GetRenderFragment();
	
	public abstract string GetMarkupString();
	
	
	
	public string? GetStyle()
	{
		string style = "";
		foreach (var property in GetType().GetProperties())
		{
			if (Attribute.IsDefined(property, typeof(StyleAttribute)))
			{
				style += $"{Translate(property.Name)}: {property.GetValue(this)};";
			}
		}
		return style;
	
	}
	
	public string? GetElementAttributeString()
	{
		string style = "";
		foreach (var property in this.GetType().GetProperties())
		{
			if (Attribute.IsDefined(property, typeof(HtmlAttribute)))
			{
				style += $"{Translate(property.Name)}=\"{property.GetValue(this)}\" ";
			}
		}
		return style;
	}
	
	public Dictionary<string, object> GetElementAttributeDictionary()
	{
		Dictionary<string, object> attributes = new();
		foreach (var property in GetType().GetProperties())
		{
			if (Attribute.IsDefined(property, typeof(HtmlAttribute)))
			{
				var value = property.GetValue(this) ?? "";
				attributes.Add(Translate(property.Name), value);
			}
		}
		return attributes;
	}
	
	
	
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
	
	public static string Translate(string key)
	{
		// for EventHandlers
		// everything that starts with an "On" and has a capital letter after that
		// will be translated to "on{lowercase}" with an @ in front, because blazor
		// e.g. OnClick -> @onclick
		// So just make sure that you name everything correctly or else everything will break
		if (key[..2] == "On" && char.IsUpper(key[2]))
		{
			return $"@on{key[2..].ToLower()}";
		}
		else
		{
			return ConvertCamelToKebab(key);
		}
	}
	
}

// Some classes like ImgButton, PolygonButton, RectButton are some Ideas I had
// They are set as properties in a  MinigameBase instance and are then
// "generated" in the markup of the Minigame
// Should have functions like check for click, disable/enable, Set/GetPos, show/hide et.

public class Rectangle : SVGElement
{
	[Html] public string? Id { get; set; }
	
	[Html] public int? X { get; set; }
	[Html] public int? Y { get; set; }
	[Html] public int Width { get; set; }
	[Html] public int Height { get; set; }
	
	public override RenderFragment GetRenderFragment()
	{
		return builder => 
		{
			builder.OpenElement(0, "rect");
			builder.AddMultipleAttributes(1, GetElementAttributeDictionary());
			builder.AddAttribute(2, "style", Style);
			builder.CloseElement();
		};
	}
	
	public override string GetMarkupString()
	{
		return $"<rect {GetElementAttributeString() ?? ""} style=\"{Style ?? ""}\"></rect>";
	}
}

public class MiniTest : MinigameBase
{
	public override string BackgroundImage { get; set; } = "images/HM305_beamer.jpg";
	
	[Element] public Rectangle Rect {get; set;} = new()
	{
		X = 100,
		Y = 100,
		Width = 100,
		Height = 100,
		ZIndex = 1,
		
	};
}