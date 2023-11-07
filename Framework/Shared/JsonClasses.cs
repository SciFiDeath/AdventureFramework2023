namespace Framework.Slides.JsonClasses;

public class JsonSlide
{
	public string? Image { get; set; }
	public List<JsonButton>? Buttons { get; set; }
}

public class JsonButton
{
	public string? Id { get; set; }
	public string? Points { get; set; }
	public string? Image { get; set; }
	public List<List<string>>? Actions { get; set; }
}