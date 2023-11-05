namespace Framework.Slides.JsonClasses;

public class Slide
{
	public string? Image { get; set; }
	public List<Button>? Buttons { get; set; }
}

public class Button
{
	public string? Id { get; set; }
	public string? Points { get; set; }
	public string? Image { get; set; }
	public List<List<string>>? Actions { get; set; }
}