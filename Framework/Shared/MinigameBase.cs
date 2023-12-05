using Microsoft.AspNetCore.Components;
using System;
namespace Framework.Minigames;


public abstract class MinigameBase : ComponentBase
{
	protected List<SVGElement> Elements { get; set; } = new();
	

	protected override void OnInitialized()
	{
		// create a list with all the elements in the Minigame
		// so that we don't have to use reflection every time
		// the thing renders
		foreach (var property in GetType().GetProperties())
		{
			if (property.GetType().IsSubclassOf(typeof(SVGElement)))
			{
				Elements.Add(property.GetValue(this));
			}
		}
		
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
	
	// public abstract RenderFragment GetElement();
	
		
	public string? GetStyle()
	{
		string style = "";
		foreach (var property in GetType().GetProperties())
		{
			if (Attribute.IsDefined(property, typeof(StyleAttribute)))
			{
				style += $"{ConvertCamelToKebab(property.Name)}: {property.GetValue(this)};";
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
				style += $"{ConvertCamelToKebab(property.Name)}=\"{property.GetValue(this)}\" ";
			}
		}
		return style;
	
	}
	
	public static string ConvertCamelToKebab(string camelCase)
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
	
public class Thing : SVGElement
{
	
}
	





// Some classes like ImgButton, PolygonButton, RectButton are some Ideas I had
// They are set as properties in a  MinigameBase instance and are then
// "generated" in the markup of the Minigame
// Should have functions like check for click, disable/enable, Set/GetPos, show/hide et.