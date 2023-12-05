using Framework.Shared;
using Microsoft.AspNetCore.Components;
using System;
namespace Framework.Minigames;

public class MinigameBase : ComponentBase
{
	[Parameter] public MinigameDefBase? Minigame { get; set; }

    protected override void OnInitialized()
    {
        
    }
}

public abstract class MinigameDefBase
{
	protected List<SVGElement> Elements { get; set; } = new();
	
	public abstract string BackgroundImage { get; set; }
	
	public MinigameDefBase()
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

public class MiniTest : MinigameDefBase
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