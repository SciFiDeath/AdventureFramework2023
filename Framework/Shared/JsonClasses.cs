namespace Framework.Slides.JsonClasses;

public class JsonSlide
{
	// public string? Image { get; set; }
	// public Dictionary<string, JsonButton>? Buttons { get; set; }

	// public string? Type { get; set; }

	// public string? MinigameDefClassName { get; set; }
	// // public List<List<string>>? OnFinishActions { get; set; }
	// public string? FallbackSlide { get; set; }

	public string? Type { get; set; }
	public List<string>? Tags { get; set; }
	public string? Image { get; set; }
	public Dictionary<string, JsonButton>? Buttons { get; set; }
	public List<List<string>>? OnEnter { get; set; }

	// Temporary for Minigame Compatibility
	public string? MinigameDefClassName { get; set; }
	public string? FallbackSlide { get; set; }
}

public class JsonButton
{
	// public string? Type { get; set; }
	// public string Points { get; set; } = null!;
	// public string? Image { get; set; }
	// public List<List<string>>? Actions { get; set; }

	public string? Type { get; set; }
	public List<string>? Tags { get; set; }
	public string? Points { get; set; }
	public string? Image { get; set; }
	public List<List<string>>? Actions { get; set; }
	public bool? Visible { get; set; }
	public int? ZIndex { get; set; }
}