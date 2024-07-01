using Framework.Mouse;

namespace Framework.Minigames.MinigameDefClasses;

public class ChemieMinigame2 : MinigameDefBase
{
	public override string BackgroundImage { get; set; } = "minigame_assets/ChemieKeller_assets/note_area_reference.png";

	static readonly string PathPrefix = "minigame_assets/ChemieKeller_assets/";

	public override string DefaultRoute { get; set; } = "ChemieKeller_w";

	[Element]
	public CustomObject RecipeOverlay { get; set; }

	[Element]
	public Polygon RecipePolygon { get; set; }

	[Element]
	public CustomObject BeakerClipPath { get; set; }

	[Element]
	public Image Beaker { get; set; }

	[Element]
	public CustomObject BeakerLiquidPattern { get; set; }

	[Element]
	public Rectangle BeakerLiquid { get; set; }

	[Element]
	public Image Pipette { get; set; }

	[Element]
	public Image PipetteShadow { get; set; }

	[Element]
	public Polygon PipettePolygon { get; set; }

	public GameObjectContainer<Ellipse> Lumps { get; set; } = new();

	// public int[][] LumpsPositions = [[1065, 868], [1148, 905], [1258, 850], [1112, 821], [1198, 779]];
	// play around with those to find suitable values
	public int[] LumpsRXRange = [13, 20];
	public int[] LumpsRYRange = [8, 15];

	static readonly int[][] MaxPos1 = [[1052, 1279], [723, 895]]; // [[x1, x2], [y1, y2]]
	static readonly int[][] MaxPos2 = [[1079, 1249], [896, 923]]; // [[x1, x2], [y1, y2]]

	readonly int[][][] MaxPos = [MaxPos1, MaxPos1, MaxPos1, MaxPos1, MaxPos2];

	public bool PipetteMode = false;

	public Random Random = new();

	public ChemieMinigame2()
	{
		// backbutton
		BackButton.Visible = true;

		Image Recipe = new()
		{
			ImagePath = PathPrefix + "recipe.png",
			X = 451,
			Y = -131,
			Width = 712,
			Height = 949,
		};

		Image Overlay = new()
		{
			ImagePath = PathPrefix + "overlay.png",
			X = 0,
			Y = 0,
			Width = 1620,
			Height = 1080,
			Visible = false,
			OnClick = (e) => { RecipeOverlay!.Visible = false; Update(); },
		};

		RecipeOverlay = new()
		{
			CustomTagName = "g",
			Content = [Overlay, Recipe],
			Visible = false,
			ZIndex = 100, // needs to be on top of everything
		};

		RecipePolygon = new()
		{
			Points = [[839, 641], [885, 631], [949, 658], [965, 800], [906, 826], [855, 807], [732, 797], [745, 635]],
			Fill = "transparent",
			Cursor = "pointer",
			OnClick = (e) => { RecipeOverlay!.Visible = true; Update(); },
			ZIndex = 5,
		};

		BeakerClipPath = new()
		{
			CustomTagName = "clipPath",
			Id = "beakerClipPath",
			Content = [
				new Polygon()
				{
					Points = [[1308,903],[1304,914],[1288,927],[1264,939],[1235,946],[1196,950],[1138,950],[1105,946],[1066,939],[1046,930],[1031,917],[1025,900],[1025,585],[1308,582]]
				}
			]
		};

		Beaker = new()
		{
			ImagePath = PathPrefix + "beaker.png",
			X = 948,
			Y = 449,
			Width = 881,
			Height = 533,
			ZIndex = 2,
			CustomStyle = "pointer-events: none;"
		};

		BeakerLiquidPattern = new()
		{
			CustomTagName = "pattern",
			Id = "beakerLiquidPattern",
			Attributes = new()
			{
				["viewBox"] = "0,0,10,10",
				["width"] = "50",
				["height"] = "50",
				["patternUnits"] = "userSpaceOnUse",

			},
			Content = [
				new Image()
				{
					ImagePath = PathPrefix + "pattern.png",
					X = 0,
					Y = 0,
					Width = 10,
					Height = 10,
				}
			]
		};

		BeakerLiquid = new()
		{
			X = 1026,
			Y = 700,
			Width = 300,
			Height = 318,
			Fill = "url(#beakerLiquidPattern)",
			ZIndex = 0,
			Attributes = new()
			{
				["clip-path"] = "url(#beakerClipPath)"
			}
		};

		Pipette = new()
		{
			ImagePath = PathPrefix + "pipette_crop.png",
			X = 284,
			Y = 442,
			Width = 331,
			Height = 436,
			ZIndex = 3,
			CustomStyle = "pointer-events: none;",
			Cursor = "none",
		};

		PipetteShadow = new()
		{
			// ImagePath = PathPrefix + "pipette_shadow.png", // funny results
			ImagePath = PathPrefix + "pipette_crop.png",
			X = 284,
			Y = 442,
			Width = 331,
			Height = 436,
			ZIndex = 2,
			Opacity = 0.5,
		};

		PipettePolygon = new()
		{
			Points = [[283, 470], [290, 449], [306, 441], [327, 453], [380, 523], [384, 546], [513, 718], [610, 867], [602, 874], [489, 738], [360, 562], [343, 555]],
			Fill = "transparent",
			Cursor = "pointer",
			ZIndex = 4,
			OnClick = PipetteHandler,
		};

		foreach (int[][] maxPos in MaxPos)
		{
			string id = Guid.NewGuid().ToString("N");
			var cX = Random.Next(maxPos[0][0], maxPos[0][1]);
			var cY = Random.Next(maxPos[1][0], maxPos[1][1]);
			Ellipse lump = new()
			{
				Id = id,
				CX = cX,
				CY = cY,
				RX = Random.Next(LumpsRXRange[0], LumpsRXRange[1]),
				RY = Random.Next(LumpsRYRange[0], LumpsRYRange[1]),
				Fill = "white",
				Stroke = "black",
				StrokeWidth = 1,
				ZIndex = 1,
				// CustomStyle = $"transform: rotate({Random.Next(0, 360)}deg);",
				Attributes = new()
				{
					["transform"] = $"rotate({Random.Next(0, 360)} {cX} {cY})"
				},
				OnClick = (e) => { if (PipetteMode) { Lumps.KillId(id); Update(); TryFinish(); } },
			};
			Lumps.Add(lump);
			AddElement(lump);
		}
		//* cannot be done in constructor, as MouseService gets attached after that
		// MouseService.SetDelay(0);

	}

	//* God I am a Genius
	//* I didn't even need to make this, but I had done it while writing the whole thing
	//* At that time I didn't even have a concrete use-case, but now this is perfect
	public override void AfterInit()
	{
		MouseService.SetDelay(0);
	}

	public void PipetteHandler(EventArgs e)
	{
		if (PipetteMode)
		{
			PipetteMode = false;
			Pipette.X = PipetteShadow.X;
			Pipette.Y = PipetteShadow.Y;
		}
		else
		{
			PipetteMode = true;
			Pipette.X = MouseService.MouseState.X - Pipette.Width;
			Pipette.Y = MouseService.MouseState.Y - Pipette.Height;
		}
		Update();
	}

	public void TryFinish()
	{
		if (Lumps.Count == 0)
		{
			GameState.RemoveItem("nitrogly_beaker");
			GameState.AddItem("nitro_pipette");
			Finish([["Sleep", "1000"], ["Route", "ChemieKeller_w"]]);
		}
	}

	public override void OnMouseMove(object? sender, MouseState e)
	{
		if (PipetteMode)
		{
			Pipette.X = e.X - Pipette.Width;
			Pipette.Y = e.Y - Pipette.Height;
			Update();
		}
	}

}
