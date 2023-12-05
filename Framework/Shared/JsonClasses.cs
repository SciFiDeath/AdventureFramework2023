namespace Framework.Slides.JsonClasses;

public class JsonSlide
{
	public string Image { get; set; } = null!;
	public Dictionary<string, JsonButton> Buttons { get; set; } = null!;
}

public class JsonButton
{
	public string Points { get; set; } = null!;
	public string? Image { get; set; }
	public List<List<string>>? Actions { get; set; }
}