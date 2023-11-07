// AT THE MOMENT THIS IS ACTUALLY USELESS, BUT MAYBE I WILL GET THE DEPENDENCY INJECTION TO WORK LATER
using Framework.Slides.JsonClasses;
using System.Net.Http.Json;

namespace Framework.Slides;

// public interface ISlidesDeserializer
// {
// 	Task<Dictionary<string, JsonSlide>> GetSlides(string url);
// 	Task<Dictionary<string, JsonSlide>> GetSlidesUnsafe(string url);
// }

public class SlidesDeserializer
{
	private readonly HttpClient _httpClient;
	
	public SlidesDeserializer(HttpClient httpClient)
	{
		_httpClient = httpClient;
	}
	
	private async Task<Dictionary<string, JsonSlide>> FetchSlidesAsync(string url)
	{
		// assign return value from GetFromJsonAsync to slides if it is not null, otherwise throw an exception
		var slides = await _httpClient.GetFromJsonAsync<Dictionary<string, JsonSlide>>(url) ?? throw new Exception("Slides is null");
		return slides;
	}
	
	private static void VerifySlides(Dictionary<string, JsonSlide> slides)
	{
		// idk, check every value to make sure it's not null
		// if you want no buttons/action, just set Buttons/Actions to an empty list
		// NOTE: This is just a null check, it doesn't check if the values are valid
		// which could result in runtime errors
		slides.Keys.ToList().ForEach(key =>
		{
			var slide = slides[key];
			if (slide.Image == null)
			{
				throw new Exception($"Slide {key} has a null image");
			}
			if (slide.Buttons == null)
			{
				throw new Exception($"Slide {key} has a null buttons");
			}
			slide.Buttons.ForEach(button =>
			{
				if (button.Id == null)
				{
					throw new Exception($"Slide {key} has a button with a null id");
				}
				if (button.Points == null && button.Image == null)
				{
					throw new Exception($"Slide {key} has a button with a null points and null image");
				}
				if (button.Actions == null)
				{
					throw new Exception($"Slide {key} has a button with a null actions");
				}
			});
		});
	}
	
	public async Task<Dictionary<string, JsonSlide>> GetSlides(string url)
	{
		var slides = await FetchSlidesAsync(url);
		try
		{
			VerifySlides(slides);
			return slides;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
	
	// doesn't verify slides, is faster but could result in unexpected behavior
	public async Task<Dictionary<string, JsonSlide>> GetSlidesUnsafe(string url)
	{
		return await FetchSlidesAsync(url);
	}
}