using Microsoft.AspNetCore.Components;
using Framework.Slides.JsonClasses;
using System.Diagnostics.CodeAnalysis;

namespace Framework.Game.Parameters;

//! Interface could be useful if we make it so that both slide and minigame
//! implement the same interface. But idk, as of now, it is not neccessary
[Obsolete(
	@"Passes the entire JsonSlide, new version passes only the Id.
	Also, I don't even know if I need this interface at all, 
	because the way the game will work has changed a lot since I wrote this."
)]
public interface IOldSlideComponentParameters
{
	JsonSlide SlideData { get; set; }
	EventCallback<string> OnSlideChange { get; set; }
}

public class ComponentParameters
{
	public Dictionary<string, object?> GetParameterDictionary()
	{
		Dictionary<string, object?> parameters = new();
		foreach (var property in this.GetType().GetProperties())
		{
			parameters.Add(property.Name, property.GetValue(this));
		}
		return parameters;
	}
}

[Obsolete("Passes the entire JsonSlide, new version passes only the Id.")]
public class OldSlideComponentParameters : ComponentParameters, IOldSlideComponentParameters
{
	public JsonSlide SlideData { get; set; } = null!;
	public EventCallback<string> OnSlideChange { get; set; }
}

public class SlideComponentParameters : ComponentParameters
{
	public string SlideId { get; set; } = null!;
	public EventCallback<string> OnSlideChange { get; set; }
}