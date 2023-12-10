namespace Framework.Slides.JsonClasses;

public class JsonSlide
{
	public string? Image { get; set; }
	public Dictionary<string, JsonButton>? Buttons { get; set; }
	
	public string? Type { get; set; }
	public List<List<string>>? OnFinishActions { get; set; }
}

public class JsonButton
{
	public string Points { get; set; } = null!;
	public string? Image { get; set; }
	public List<List<string>>? Actions { get; set; }
}