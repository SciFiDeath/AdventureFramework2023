using Framework.Minigames;
using Microsoft.AspNetCore.Components.Web;

namespace Framework.Minigames.MinigameDefClasses;

public class Demo : MinigameDefBase
{
	public override string BackgroundImage { get; set; } = "images/HM305_blackboard.jpg";

	public void Test(EventArgs e)
	{
		Rect.X += 20;
		Update();
		if (Rect.X > 600)
		{
			Finish(null, "HM305Beamer");
		}
	}

	[Element] public Rectangle Rect { get; set; }

	public Demo()
	{
		Rect = new()
		{
			X = 100,
			Y = 100,
			Width = 100,
			Height = 100,
			ZIndex = 1,
			Fill = "red",
			OnClick = Test
		};
	}
}