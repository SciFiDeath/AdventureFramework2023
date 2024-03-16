using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Components;

namespace Framework.Minigames;


public interface IGameObject
{
	public string Id { get; set; }

	// for render order
	public int? ZIndex { get; set; }

	// visibilty not over style, but directly when rendering
	public bool Visible { get; set; }
	public RenderFragment GetRenderFragment();

	public event EventHandler? OnKill;
	public void Kill();
}

// implementing this interface causes `Style` to break cause there is somehow
// an ambiguity between the property defined in the class and the on in the interface
// public interface ISVGElement : IGameObject
// {
// 	string TagName { get; }
// 	string? Style { get; }
// 	public int? ZIndex { get; set; }
// }


// you can use this as a base class, but you have to make the
// GetRenderFragment method yourself
public abstract class GameObject : IGameObject
{
	// Make sure that every element always has an Id
	public string Id { get; set; } = Guid.NewGuid().ToString("N");

	public virtual int? ZIndex { get; set; }

	public virtual bool Visible { get; set; } = true;

	public event EventHandler? OnKill;

	public void Kill()
	{
		OnKill?.Invoke(this, EventArgs.Empty);
	}

	public abstract RenderFragment GetRenderFragment();
}


// This is the main class you should use for your custom game objects
// It already includes the rendering logic
// Basically just a wrapper for an SVGElement
// Abstracted away here so that you don't have to subclass the SVGElement classes
// directly, but can use this as a base class if you want to add stuff
public abstract class SVGElementGameObject : GameObject
{
	// it is mandatory to set the element, otherwise you could just subclass GameObject
	public SVGElement Element { get; set; } = null!;

	public override RenderFragment GetRenderFragment()
	{
		return Element.GetRenderFragment();
	}

	public string? Style { get => Element.Style; }

	public override int? ZIndex
	{
		get => Element.ZIndex;
		set => Element.ZIndex = value;
	}

	public string TagName { get => Element.TagName; }
}


public abstract class SVGElement : GameObject
{
	[Html("style")] public string? Style { get => GetStyleString(); }

	// Normal implementation (maybe slightly slower, but I understand it better)
	public abstract string TagName { get; }
	// maybe virtual for more custom elements?
	// public virtual string TagName { get; } = "div";


	public virtual string? CustomStyle { get; set; }

	// some properties that are the same across all elements
	//  event handlers, as they are the same over all elements
	[Callback("onclick")] public Action<EventArgs>? OnClick { get; set; }
	[Callback("ondblclick")] public Action<EventArgs>? OnDoubleClick { get; set; }
	[Callback("onmouseenter")] public Action<EventArgs>? OnMouseEnter { get; set; }
	[Callback("onmouseleave")] public Action<EventArgs>? OnMouseLeave { get; set; }

	// set the cursor (probably most often to "pointer")
	[Style("cursor")] public string? Cursor { get; set; }
	[Style("opacity")] public double? Opacity { get; set; }




	public override RenderFragment GetRenderFragment()
	{
		return builder =>
		{
			builder.OpenElement(0, TagName);
			builder.AddMultipleAttributes(1, GetElementAttributeDictionary());
			builder.AddMultipleAttributes(2, GetCallbackDictionary());
			builder.CloseElement();
		};
	}

	// // [Obsolete("Strings cannot contain blazor stuff, use RenderFragments instead")]
	// // public abstract string GetMarkupString();

	// maybe problems with reactivity, don't know yet
	[Obsolete("Not used, no reason to ever use, there is another that does the same thing")]
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
		PropertyInfo[] properties = GetType()
		.GetProperties(
		BindingFlags.Instance |
		BindingFlags.Public |
		BindingFlags.NonPublic
		)
		.Where(p => Attribute.IsDefined(p, typeof(StyleAttribute)))
		.ToArray();
		foreach (var property in properties)
		{
			var value = property.GetValue(this);
			if (value != null)
			{
				//*Note: Added null suppression because safety is ensured, but keep in mind regardless
				// with translate {...ibute<StyleAttribute>()!.name ?? Translate(property.Name)}
				style +=
				$"{property.GetCustomAttribute<StyleAttribute>()!.name}: {value};";
			}
		}
		// add custom style
		style += CustomStyle;
		return style == "" ? null : style;
	}

	[Obsolete("MarkupStrings cannot contain blazor stuff, use RenderFragments instead")]
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

	[Obsolete("MarkupStrings cannot contain blazor stuff, use RenderFragments instead")]
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
		PropertyInfo[] properties = GetType()
		.GetProperties(
			BindingFlags.Instance |
			BindingFlags.Public |
			BindingFlags.NonPublic
		)
		.Where(p => Attribute.IsDefined(p, typeof(HtmlAttribute)))
		.ToArray();
		foreach (var property in properties)
		{
			var value = property.GetValue(this);

			if (value != null)
			{
				attributes.Add(
					//*Note: Added null suppression because safety is ensured, but keep in mind regardless
					property.GetCustomAttribute<HtmlAttribute>()!.name,
					value
				);
			}
		}
		return attributes.Count == 0 ? null : attributes;
	}

	public Dictionary<string, object>? GetCallbackDictionary()
	{
		Dictionary<string, object> callbacks = [];
		PropertyInfo[] methods = GetType()
		.GetProperties(
			BindingFlags.Instance |
			BindingFlags.Public |
			BindingFlags.NonPublic
		)
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
					method.GetCustomAttribute<CallbackAttribute>()!.name,
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

	[Obsolete("Not used and also a bodgy solution, use the AttrubteClasses's `name` property instead")]
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

	[Obsolete("Html and Style attrs now require `name`, this is just a possible error source")]
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

public abstract class ShapeElement : SVGElement
{
	[Style("fill")] public string? Fill { get; set; }
	[Style("fill-opacity")] public double? FillOpacity { get; set; }
	// stroke stuff
	[Style("stroke")] public string? Stroke { get; set; }
	[Style("stroke-width")] public int? StrokeWidth { get; set; }
	[Style("stroke-opacity")] public double? StrokeOpacity { get; set; }
}


// Some classes like ImgButton, PolygonButton, RectButton are some Ideas I had
// They are set as properties in a  MinigameBase instance and are then
// "generated" in the markup of the Minigame
// Should have functions like check for click, disable/enable, Set/GetPos, show/hide et.

public class Polygon : ShapeElement
{
	public override string TagName { get; } = "polygon";

	public List<int[]> Points { get; set; } = [];

	[Html("points")]
	private string PointString => string.Join(",", Points.SelectMany(i => i));

	// [Style("fill")] public string? Fill { get; set; }
}

public class Polyline : ShapeElement
{
	public override string TagName { get; } = "polyline";

	public List<int[]> Points { get; set; } = [];

	[Html("points")]
	private string PointString => string.Join(",", Points.SelectMany(i => i));
}

public class Rectangle : ShapeElement
{
	// // Implementation of the TagName property as static
	// public new static string TagName { get; } = "rect";

	// "normal" implementation of TagName
	public override string TagName { get; } = "rect";

	[Html("x")] public int? X { get; set; }
	[Html("y")] public int? Y { get; set; }
	[Html("width")] public int Width { get; set; }
	[Html("height")] public int Height { get; set; }

	// [Style("fill")] public string? Fill { get; set; }

	// now inherited
	// [Callback("onclick")] public Action<EventArgs>? OnClick { get; set; }
}

public class Circle : ShapeElement
{
	public override string TagName { get; } = "circle";
	[Html("cx")] public int? CX { get; set; }
	[Html("cy")] public int? CY { get; set; }
	[Html("r")] public int? R { get; set; }
	[Html("pathLength")] public int? PathLength { get; set; }
}

public class Ellipse : ShapeElement
{
	public override string TagName { get; } = "ellipse";
	[Html("cx")] public int? CX { get; set; }
	[Html("cy")] public int? CY { get; set; }
	[Html("rx")] public int? RX { get; set; }
	[Html("ry")] public int? RY { get; set; }
	[Html("pathLength")] public int? PathLength { get; set; }
}

public class Line : ShapeElement
{
	public override string TagName { get; } = "line";
	[Html("x1")] public int? X1 { get; set; }
	[Html("x2")] public int? X2 { get; set; }
	[Html("y1")] public int? Y1 { get; set; }
	[Html("y2")] public int? Y2 { get; set; }
	[Html("pathLength")] public int? PathLength { get; set; }
	// I know that fill isn't technically valid for line, but css will just ignore it
}

public class Text : SVGElement
{
	public override string TagName { get; } = "text";

	public string? InnerText { get; set; }

	[Html("x")] public int? X { get; set; }
	[Html("y")] public int? Y { get; set; }
	[Html("dx")] public int? DX { get; set; }
	[Html("dy")] public int? DY { get; set; }
	[Html("rotate")] public int? Rotate { get; set; }
	[Html("textLength")] public int? TextLength { get; set; }

	[Style("fill")] public string? Fill { get; set; }

	[Html("lengthAdjust")]
	private string? LengthAdjust => StretchLetters is true ? "spacingAndGlyphs" : null;
	public bool StretchLetters { get; set; }

	[Style("font-size")]
	private string? FontSizeString => FontSize != null ? $"{FontSize}px" : null;
	public int? FontSize { get; set; }

	[Style("font-family")] public string? FontFamily { get; set; }

	[Style("user-select")] private string? UserSelect => Selectable != true ? "none" : null;
	public bool? Selectable { get; set; }

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

public class Image : SVGElement
{
	public override string TagName { get; } = "image";

	[Html("x")] public int? X { get; set; }
	[Html("y")] public int? Y { get; set; }
	[Html("width")] public int? Width { get; set; }
	[Html("height")] public int? Height { get; set; }

	// [Style("display")] public string? Visibility { get; set; }

	[Html("href")] public string? ImagePath { get; set; }

	// now inherited
	// [Callback("onclick")] public Action<EventArgs>? OnClick { get; set; }

}