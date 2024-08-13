using System.Configuration;
using Microsoft.AspNetCore.Components;

namespace Framework.Minigames.MinigameDefClasses;

public class ChemieMinigame1 : MinigameDefBase
{
	static readonly string PathPrefix = "minigame_assets/ChemieKeller_assets/";

	public override string BackgroundImage { get; set; } = PathPrefix + "note_area_reference.png";

	public override string DefaultRoute { get; set; } = "ChemieKeller_w";

	// needed elements
	[Element]
	public CustomObject BuretClipPath { get; set; }

	[Element]
	public CustomObject BeakerClipPath { get; set; }

	[Element]
	public Image Pipette { get; set; }

	[Element]
	public Polygon PipettePolygon { get; set; }

	[Element]
	public Image Buret { get; set; }

	[Element]
	public Image Beaker { get; set; }

	[Element]
	public Image NitricAcid { get; set; }

	[Element]
	public Image SulfuricAcid { get; set; }

	[Element]
	public CustomObject BuretLiquidPattern { get; set; }

	[Element]
	public CustomObject BeakerLiquidPattern { get; set; }

	[Element]
	public CustomObject BeakerLiquidPattern2 { get; set; }

	[Element]
	public Rectangle BuretLiquid { get; set; }

	[Element]
	public Rectangle BeakerLiquid { get; set; }

	[Element]
	public Rectangle BeakerLiquid2 { get; set; }

	[Element]
	public Rectangle StopButton { get; set; }

	[Element]
	public Text StopButtonText { get; set; }

	[Element]
	public Rectangle ConfirmButton { get; set; }

	[Element]
	public Text ConfirmButtonText { get; set; }

	[Element]
	public Polygon RecipePolygon { get; set; }

	// [Element]
	// public Image Recipe { get; set; }

	// [Element]
	// public Image Overlay { get; set; }
	[Element]
	public CustomObject RecipeOverlay { get; set; }


	public GameObjectContainer<SVGElement> BuretElements { get; set; } = new();
	public GameObjectContainer<SVGElement> BeakerElements { get; set; } = new();
	public GameObjectContainer<Circle> Droplets { get; set; } = new();

	static readonly string[] PatternPaths = [
		PathPrefix + "pattern.png",
		PathPrefix + "pattern_blue.png",
		PathPrefix + "pattern_yellow.png",
		PathPrefix + "pattern_green.png",
	];

	public ChemieMinigame1()
	{
		// backbutton
		BackButton.Visible = true;

		// set values
		BuretClipPath = new()
		{
			CustomTagName = "clipPath",
			Id = "buretClipPath",
			Content = [
				new Polygon()
				{
					Points = [[470,682],[490,685],[519,687],[544,684],[543,21],[472,21]]
				}
			]
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

		PipettePolygon = new()
		{
			Points = [[1158, 466], [1165, 460], [1062, 304], [946, 144], [935, 135], [935, 117], [883, 45], [863, 32], [848, 36], [838, 56], [899, 145], [916, 151], [1045, 330]],
			Fill = "transparent",
			Cursor = "pointer",
			Visible = false,
			ZIndex = 4,
			OnClick = PipetteHandler
		};

		Pipette = new()
		{
			ImagePath = PathPrefix + "pipette.png",
			X = 623,
			Y = -59,
			Width = 775,
			Height = 615,
			Visible = false,
			ZIndex = 3,
		};

		Buret = new()
		{
			ImagePath = PathPrefix + "buret.png",
			X = 467,
			Y = -57,
			Width = 129,
			Height = 867,
			ZIndex = 2,
		};

		Beaker = new()
		{
			ImagePath = PathPrefix + "beaker.png",
			X = 948,
			Y = 449,
			Width = 881,
			Height = 533,
			ZIndex = 2,
		};

		NitricAcid = new()
		{
			ImagePath = PathPrefix + "nitric_acid.png",
			X = 288,
			Y = 572,
			Width = 715,
			Height = 714,
			ZIndex = 0
		};

		SulfuricAcid = new()
		{
			Id = "sulfuricAcid",
			ImagePath = PathPrefix + "sulfuric_acid.png",
			X = 291,
			Y = 574,
			Width = 705,
			Height = 705,
			ZIndex = 0
		};

		//* Do patterns later

		BuretLiquidPattern = new()
		{
			CustomTagName = "pattern",
			Id = "buretLiquidPattern",
			Attributes = new()
			{
				["viewBox"] = "0,0,10,10",
				["width"] = "25",
				["height"] = "25",
				["patternUnits"] = "userSpaceOnUse",

			},
			Content = [
				new Image()
				{
					ImagePath = PatternPaths[2],
					X = 0,
					Y = 0,
					Width = 10,
					Height = 10,
				}
			]
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
					ImagePath = PatternPaths[2],
					X = 0,
					Y = 0,
					Width = 10,
					Height = 10,
				}
			]
		};

		BeakerLiquidPattern2 = new()
		{
			CustomTagName = "pattern",
			Id = "beakerLiquidPattern2",
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
					ImagePath = PatternPaths[0],
					X = 0,
					Y = 0,
					Width = 10,
					Height = 10,
				}
			]
		};

		BuretLiquid = new()
		{
			X = 473,
			Y = BuretStartY,
			Width = 71,
			Height = 700,


			Fill = "url(#buretLiquidPattern)",
			ZIndex = 1,
			Attributes = new()
			{
				["clip-path"] = "url(#buretClipPath)"
			}
		};

		BeakerLiquid = new()
		{
			X = 1026,
			Y = BeakerStartY,
			Width = 300,
			Height = 318,
			Fill = "url(#beakerLiquidPattern)",
			ZIndex = 0,
			Attributes = new()
			{
				["clip-path"] = "url(#beakerClipPath)"
			}
		};

		BeakerLiquid2 = new()
		{
			X = 1026,
			Y = BeakerNitricY,
			Width = 300,
			Height = 318,
			Fill = "url(#beakerLiquidPattern2)",
			ZIndex = 1,
			Opacity = 0,
			Attributes = new()
			{
				["clip-path"] = "url(#beakerClipPath)"
			}
		};


		StopButton = new()
		{
			X = 131,
			Y = 327,
			Width = 200,
			Height = 100,
			Fill = "red",
			Cursor = "pointer",
			OnClick = StopHandler,
		};

		StopButtonText = new()
		{
			X = 158,
			Y = 395,
			InnerText = "Stop",
			Fill = "white",
			FontSize = 60,
			CustomStyle = "pointer-events: none;"
		};

		ConfirmButton = new()
		{
			X = 1300,
			Y = 327,
			Width = 300,
			Height = 100,
			Fill = "green",
			Cursor = "pointer",
			Visible = false,
			OnClick = ConfirmHandler,
		};

		ConfirmButtonText = new()
		{
			X = 1327,
			Y = 395,
			InnerText = "Bestätigen",
			Fill = "white",
			FontSize = 60,
			CustomStyle = "pointer-events: none;",
			Visible = false,
		};

		RecipePolygon = new()
		{
			Points = [[839, 641], [885, 631], [949, 658], [965, 800], [906, 826], [855, 807], [732, 797], [745, 635]],
			Fill = "transparent",
			Cursor = "pointer",
			OnClick = (e) => { RecipeOverlay!.Visible = true; Update(); },
			ZIndex = 5,
		};

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

		// add elements to containers
		BuretElements.AddMultiple([
			Buret,
			BuretLiquid,
			NitricAcid,
			SulfuricAcid,
			StopButton,
			StopButtonText,
		]);

		BeakerElements.AddMultiple([
			Pipette,
			PipettePolygon,
			ConfirmButton,
			ConfirmButtonText,
		]);
	}

	// actual minigame logic

	public void Reset()
	{
		// reset modes
		Mode = "buret";
		BuretMode = "sulfuric";

		// reset buret game params
		Start = true;
		BuretDirection = -1;
		Random = new(); // why not

		// reset droplets
		Droplets.KillAll();
		DropletCounter = 0;
		BeakerLiquid2.Opacity = 0;

		// reset coords of stuff
		BeakerLiquid.Y = BeakerStartY;

		// reset patterns
		Image Pattern = (Image)BuretLiquidPattern.Content[0];
		Pattern.ImagePath = PatternPaths[2]; // yellow
		Image BeakerPattern = (Image)BeakerLiquidPattern.Content[0];
		BeakerPattern.ImagePath = PatternPaths[2]; // yellow

		// reset visibilities
		BuretElements.Transform((e) => e.Visible = true);
		BeakerElements.Transform((e) => e.Visible = false);

		BuretLiquid.Y = BuretStartY;
		Update();
	}

	public const int SulfurTopY = 375;
	public const int SulfurBottomY = 305;

	public const int NitricTopY = 150;
	public const int NitricBottomY = 80;

	public void StopHandler(EventArgs e)
	{
		if (BuretMode == "sulfuric")
		{
			if (BuretLiquid.Y >= SulfurBottomY && BuretLiquid.Y <= SulfurTopY)
			{
				SoundService.PlaySound("minigame_assets/Music_Assets/Sounds/S.wav");
				SulfuricAcid.Visible = false;
				BuretLiquid.Y = BuretStartY;
				Start = true;
				BuretDirection = -1;
				Image Pattern = (Image)BuretLiquidPattern.Content[0];
				Pattern.ImagePath = PatternPaths[1];
				BuretMode = "nitric";

				BeakerLiquid.Y = BeakerSulfuricY;
				Image BeakerPattern = (Image)BeakerLiquidPattern.Content[0];
				BeakerPattern.ImagePath = PatternPaths[2];

				Update();
			}
			else
			{
				SoundService.PlaySound("minigame_assets/Music_Assets/Sounds/W.wav");
				// BuretLiquid.Y = BuretStartY;
				// Start = true;
				// BuretDirection = -1;
				// Update();
				Reset();
			}
		}
		else if (BuretMode == "nitric")
		{
			if (BuretLiquid.Y >= NitricBottomY && BuretLiquid.Y <= NitricTopY)
			{
				SoundService.PlaySound("minigame_assets/Music_Assets/Sounds/S.wav");
				Mode = "pipette";
				// phase transition
				BuretElements.Transform((e) => e.Visible = false);
				BeakerElements.Transform((e) => e.Visible = true);

				// move beaker
				BeakerLiquid.Y = BeakerNitricY;
				Image BeakerPattern = (Image)BeakerLiquidPattern.Content[0];
				BeakerPattern.ImagePath = PatternPaths[3];


				Update();
			}
			else
			{
				SoundService.PlaySound("minigame_assets/Music_Assets/Sounds/W.wav");
				// SulfuricAcid.Visible = true;
				// BuretMode = "sulfuric";

				// Image Pattern = (Image)BuretLiquidPattern.Content[0];
				// Pattern.ImagePath = PatternPaths[2];

				// BuretLiquid.Y = BuretStartY;
				// Start = true;
				// BuretDirection = -1;
				// Update();
				Reset();
			}
		}
	}

	public void PipetteHandler(EventArgs e)
	{
		// create droplet
		Circle Droplet = new()
		{
			CX = DropletSpawnCoords[0],
			CY = DropletSpawnCoords[1],
			R = DropletRadius,
			Fill = DropletColor,
			ZIndex = -1,
		};
		// add droplet
		Droplets.Add(Droplet);
		AddElement(Droplet);
		// increase counter
		DropletCounter++;
	}

	public void ConfirmHandler(EventArgs e)
	{
		if (DropletCounter != 5)
		{
			SoundService.PlaySound("minigame_assets/Music_Assets/Sounds/W.wav");
			Reset();
		}
		else
		{
			SoundService.PlaySound("minigame_assets/Music_Assets/Sounds/S.wav");
			// remove ingredients
			GameState.RemoveItem("sulfuric_acid");
			GameState.RemoveItem("nitric_acid");
			GameState.RemoveItem("glycerin");
			// add nitro_beaker
			GameState.AddItem("nitro_beaker");
			// show fridge polygon
			GameState.SetState("ChemieKeller_s.FridgePolygon", true);

			Finish([["Sleep", "1000"], ["Route", "ChemieKeller_w"]]);


		}
	}

	// state initialization
	public string Mode = "buret";

	public const int BuretStartY = 685;
	public const int BeakerStartY = 950;
	public const int BeakerSulfuricY = 850;
	public const int BeakerNitricY = 700;

	// constants for buret
	public const int BuretSpeed = 10;
	public const int TopY = 650;
	public const int BottomY = 20;
	public const double DirectionSwitchProbability = 1.0 / 60.0;
	// params for buret
	public int BuretDirection = -1; // 1 = down, -1 = up
	public bool Start = true;
	public string BuretMode = "sulfuric";

	// constants for pipette
	public const int DropletSpeed = 35;
	public const int DropletRadius = 7;
	public const string DropletColor = "lightgrey"; // maybe silver?
	public int[] DropletSpawnCoords = [1162, 463];
	// params for pipette
	public int DropletCounter = 0;

	public Random Random = new();

	public override async Task GameLoop(CancellationToken ct)
	{
		while (true)
		{
			await Task.Delay(1000 / 24, ct);
			if (Mode == "buret")
			{
				BuretLiquid.Y += BuretSpeed * BuretDirection;

				if (Start)
				{
					if (BuretLiquid.Y <= TopY)
					{
						Start = false;
					}
					else
					{
						Update();
						continue;
					}
				}

				if (BuretLiquid.Y <= BottomY || BuretLiquid.Y >= TopY)
				{
					BuretDirection *= -1;
				}
				else if (Random.NextDouble() < DirectionSwitchProbability)
				{
					BuretDirection *= -1;
				}
			}
			else if (Mode == "pipette")
			{
				// move drops downwards
				// kill them on contact with liquid
				Droplets.Transform(
					(e) =>
					{
						if (e.CY >= BeakerNitricY)
						{
							e.Kill();
							BeakerLiquid2.Opacity += 0.2;
							return;
						}
						e.CY += DropletSpeed;
					}
				);
			}

			Update();
			ct.ThrowIfCancellationRequested();
		}
	}

}

