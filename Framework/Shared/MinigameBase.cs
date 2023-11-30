using Microsoft.AspNetCore.Components;
using System;
namespace Framework.Minigames;

public class MinigameBase : ComponentBase
{
	

	protected Dictionary<string, object?> GetParameterDictionary()
	{
		Dictionary<string, object?> parameters = new();
		foreach (var property in this.GetType().GetProperties())
		{
			parameters.Add(property.Name, property.GetValue(this));
		}
		return parameters;
	}
}

[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
public sealed class StyleAttribute : Attribute {}

[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
public sealed class HtmlAttribute : Attribute {}

public abstract class SVGButton
{
	[Html] public string? Style { get => GetStyle(); }
	
	
	
	
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
}





// Some classes like ImgButton, PolygonButton, RectButton are some Ideas I had
// They are set as properties in a  MinigameBase instance and are then
// "generated" in the markup of the Minigame
// Should have functions like check for click, disable/enable, Set/GetPos, show/hide et.