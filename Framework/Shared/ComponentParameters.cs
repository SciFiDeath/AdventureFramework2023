using Microsoft.AspNetCore.Components;
using Framework.Slides.JsonClasses;
using System.Diagnostics.CodeAnalysis;

namespace Framework.Game.Parameters;

public interface ISlideComponentParameters
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

public class SlideComponentParameters : ComponentParameters, ISlideComponentParameters
{
	public JsonSlide SlideData { get; set; } = null!;
	public EventCallback<string> OnSlideChange { get; set; }
}