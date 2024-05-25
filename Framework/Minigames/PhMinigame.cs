/*

namespace Framework.Minigames.MinigameDefClasses;


public class Material : SVGImage
{
   
    public Rectangle PlaceHolder { get; set; }
    SVGImage HintImage { get; set; }
    public string HintImageUrl { get; set; }
    public int PlaceHolderX { get; set; }
    public int PlaceHolderY { get; set; }
    public int CorrectX { get; set; }
    public int CorrectY { get; set; }
    public int CurrentX { get; set; } = -1;
    public int CurrentY { get; set; } = -1;
    public void OnHintClick(EventArgs args)
    {
        
        CurrentX = CorrectX;
        CurrentY = CorrectY;

       
        Game.PlaceImageToWhiteBoardCenter(this, Game.WhiteBoardRectangles[CorrectX, CorrectY]);


        Game.CheckIsFinished();
    }

    public void OnMaterialClick(EventArgs args)
    {
        HintImage.Visible = true;

        
        PlaceHolder.Visible = false;

        Game.PlaceImageToWhiteBoardCenter(HintImage, Game.WhiteBoardRectangles[CorrectX, CorrectY]);

       
        if (Game.SelectedMaterial != null && Game.SelectedMaterial != this) Game.SelectedMaterial.HideHint();

        Game.SelectedMaterial = this;
    }
  
    public void HideHint()
    {
        HintImage.Visible = false;

        Game.Update();
    }
 
    public void Reset()
    {
        
        PlaceHolder.Visible = true;
        
        Visible = false;
        CurrentX = -1;
        CurrentY = -1;

        HideHint();
    }
 
    public void PlaceTo(int x, int y)
    {
        Game.PlaceImageToWhiteBoardCenter(this, Game.WhiteBoardRectangles[x, y]);

        CurrentX = x;
        CurrentY = y;
    }
  
    public void AddToGame(PhMinigame Game)
    {
       
        this.Game = Game;

        Visible = false;

        OnClick = OnMaterialClick;

        PlaceHolder = new Rectangle()
        {
            X = PlaceHolderX,
            Y = PlaceHolderY,
            Width = this.Width.Value, 
            Height = this.Height.Value,
            Fill = "rgba(255,0,0,.50)",
            OnClick = OnMaterialClick 
        };


        HintImage = new()
        {
            Width = this.Width, 
            Height = this.Height, 
            Image = HintImageUrl, 
            OnClick = OnHintClick, 
            Visible = false
        };

        
        Game.AddElement(PlaceHolder);

        Game.AddElement(HintImage);

        Game.AddElement(this);

    }
   
    public PhMinigame Game { get; set; }
}

public class PhMinigame : MinigameDefBase
{
    public override string BackgroundImage { get; set; } = "images/PhMinigame/edited/Platform1.png";

    public PhMinigame()
    {
        StartGame();
    }

    public Material? SelectedMaterial { get; set; }
    
    static int ColumnCount = 6;
    static int RowCount = 8;
    int WhiteBoardRectangleWidth = 128;
    int WhiteBoardRectangleHeight = 105;
    List<Material> Materials { get; set; } = new List<Material>();

    public Rectangle[,] WhiteBoardRectangles = new Rectangle[ColumnCount, RowCount];
    void StartGame()
    {
        CreateBackgroundElement();

        CreateWhiteBoardRectangles();

        CreateMaterials();
    }
    void CreateBackgroundElement()
    {
       
        var BackgroundElement = new Rectangle()
        {
            X = 0,
            Y = 0,
            Width = 2000, 
            Height = 2000,
            Fill = "transparent",
            OnClick = (args) =>
            {
                if (SelectedMaterial == null) return;

                SelectedMaterial.Reset();

                SelectedMaterial = null;
            },
        };

        AddElement(BackgroundElement);
    }
    void CreateWhiteBoardRectangles()
    {
        for (int x = 0; x < ColumnCount; x++)
        {
            for (int y = 0; y < RowCount; y++)
            {
                Rectangle WhiteBoard = new Rectangle()
                {
                    X = (x * WhiteBoardRectangleWidth) + 380,   
                    Y = (y * WhiteBoardRectangleHeight) + 95 ,  
                    Width = WhiteBoardRectangleWidth,  
                    Height = WhiteBoardRectangleHeight,         
                    //Fill = "transparent",
                    Fill = "rgba(0,255,0,.5)", //Şeffaf yeşil test için
                };
                
                int WhiteBoardPointX = x;
                int WhiteBoardPointY = y;

                WhiteBoard.OnClick = (args) =>
                {
                    
                    if (SelectedMaterial == null) return;
                  

                    SelectedMaterial.PlaceTo(WhiteBoardPointX, WhiteBoardPointY);
                };

                AddElement(WhiteBoard);

                
                WhiteBoardRectangles[x, y] = WhiteBoard;

            }

        }
    }
    void CreateMaterials()
    {
       
        Materials =
        [
            new Material()
            {
                Id = "HorizontalStick",
                Image = "images/PhMinigame/edited/HorizontalStick.png",
                HintImageUrl = "images/PhMinigame/edited/HorizontalStick_hint.png", //%50 daha şeffaf hali orijinal resimdem
                Height = 80,
                Width = 190,
                CorrectX = 2, //Material'in tahtada olması gereken X koordinatı
                CorrectY = 3, //Material'in tahtada olması gereken Y koordinatı
                PlaceHolderX = 1430, //Placeholderın oyun alanında başlaması gereken X konumu
                PlaceHolderY = 680, //Placeholderın oyun alanında başlaması gereken Y konumu
            },
            new Material()
            {
                Id = "HorizontalStick2",
                Image = "images/PhMinigame/edited/HorizontalStick.png",
                HintImageUrl = "images/PhMinigame/edited/HorizontalStick_hint.png",
                Height = 80,
                Width = 190,
                CorrectX = 2,
                CorrectY = 1,
                PlaceHolderX = 1430,
                PlaceHolderY = 680,
            },
            new Material()
            {
                Id = "VerticalStick",
                Image = "images/PhMinigame/edited/VerticalStick.png",
                HintImageUrl = "images/PhMinigame/edited/VerticalStick_hint.png",
                Height = 100,
                Width = 190,
                CorrectX = 1,
                CorrectY = 4,
                PlaceHolderX = 1430,
                PlaceHolderY = 820,
            },
            new Material()
            {
                Id = "VerticalStick2",
                Image = "images/PhMinigame/edited/VerticalStick.png",
                HintImageUrl = "images/PhMinigame/edited/VerticalStick_hint.png",
                Height = 100,
                Width = 190,
                CorrectX = 3,
                CorrectY = 2,
                PlaceHolderX = 1430,
                PlaceHolderY = 820,
            },

            new Material()
            {
                Id = "Potentiometer",
                Image = "images/PhMinigame/edited/Potentiometer.png",
                HintImageUrl = "images/PhMinigame/edited/Potentiometer_hint.png",
                Height = 170,
                Width = 190,
                CorrectX = 3,
                CorrectY = 1,
                PlaceHolderX = 1420,
                PlaceHolderY = 30,
            },
            new Material()
            {
                Id = "OpenSwitch",
                Image = "images/PhMinigame/edited/OpenSwitch.png",
                HintImageUrl = "images/PhMinigame/edited/OpenSwitch_hint.png",
                Height = 170,
                Width = 190,
                CorrectX = 3,
                CorrectY = 3,
                PlaceHolderX = 1425,
                PlaceHolderY = 245,
            },
            new Material()
            {
                Id = "OpenSwitch2",
                Image = "images/PhMinigame/edited/OpenSwitch.png",
                HintImageUrl = "images/PhMinigame/edited/OpenSwitch_hint.png",
                Height = 170,
                Width = 190,
                CorrectX = 1,
                CorrectY = 3,
                PlaceHolderX = 1430,
                PlaceHolderY = 460,
            },
            new Material()
            {
                Id = "HorizontalLamp",
                Image = "images/PhMinigame/edited/HorizontalLamp.png",
                HintImageUrl = "images/PhMinigame/edited/HorizontalLamp_hint.png",
                Height = 80,
                Width = 190,
                CorrectX = 1,
                CorrectY = 5,
                PlaceHolderX = 1430,
                PlaceHolderY = 940,
            },
            new Material()
            {
                Id = "Battery",
                Image = "images/PhMinigame/edited/Battery.png",
                HintImageUrl = "images/PhMinigame/edited/Battery_hint.png",
                Height = 170,
                Width = 190,
                CorrectX = 1,
                CorrectY = 1,
                PlaceHolderX = 1190,
                PlaceHolderY = 78,
            },

        ];

        foreach (var material in Materials)
        {
            material.AddToGame(this);
        }

    }
    public void PlaceImageToWhiteBoardCenter(SVGImage image, Rectangle whiteBoard)
    {
        
        image.X = whiteBoard.X + whiteBoard.Width / 2 - image.Width / 2;
        image.Y = whiteBoard.Y + whiteBoard.Height / 2 - image.Height / 2;
        image.Visible = true;
       
        Update();
    }
    public void CheckIsFinished()
    {
        bool isFinished = true;

        foreach (var material in Materials)
        {
            
            if (material.CurrentX != material.CorrectX || material.CurrentY != material.CorrectY)
            {
                
                isFinished = false;
                break;
            }
        }

        if (isFinished)
        {
            Finish(true);
        }
    }

}
*/

namespace Framework.Minigames.MinigameDefClasses;
public class Material : SVGImage
{
    public Rectangle PlaceHolder { get; set; }
    public SVGImage HintImage { get; set; }
    public string HintImageUrl { get; set; }
    public int PlaceHolderX { get; set; }
    public int PlaceHolderY { get; set; }
    public int CorrectX { get; set; }
    public int CorrectY { get; set; }
    public int CurrentX { get; set; } = -1;
    public int CurrentY { get; set; } = -1;
    public PhMinigame Game { get; set; }
}

public class PhMinigame : MinigameDefBase
{
    public override string BackgroundImage { get; set; } = "images/PhMinigame/edited/Platform1.png";
    public Material? SelectedMaterial { get; set; }
    static int ColumnCount = 6;
    static int RowCount = 8;
    int WhiteBoardRectangleWidth = 128;
    int WhiteBoardRectangleHeight = 105;
    List<Material> Materials { get; set; } = new List<Material>();
    public Rectangle[,] WhiteBoardRectangles = new Rectangle[ColumnCount, RowCount];

    public PhMinigame()
    {
        StartGame();
    }

    void StartGame()
    {
        CreateBackgroundElement();
        CreateWhiteBoardRectangles();
        CreateMaterials();
    }

    void CreateBackgroundElement()
    {
        var BackgroundElement = new Rectangle()
        {
            X = 0,
            Y = 0,
            Width = 2000,
            Height = 2000,
            Fill = "transparent",
            OnClick = (args) =>
            {
                if (SelectedMaterial == null) return;
                ResetMaterial(SelectedMaterial);
                SelectedMaterial = null;
            },
        };

        AddElement(BackgroundElement);
    }

    void CreateWhiteBoardRectangles()
    {
        for (int x = 0; x < ColumnCount; x++)
        {
            for (int y = 0; y < RowCount; y++)
            {
                Rectangle WhiteBoard = new Rectangle()
                {
                    X = (x * WhiteBoardRectangleWidth) + 380,
                    Y = (y * WhiteBoardRectangleHeight) + 95,
                    Width = WhiteBoardRectangleWidth,
                    Height = WhiteBoardRectangleHeight,
                    Fill = "rgba(0,255,0,.5)",
                };

                int WhiteBoardPointX = x;
                int WhiteBoardPointY = y;

                WhiteBoard.OnClick = (args) =>
                {
                    if (SelectedMaterial == null) return;
                    PlaceMaterialTo(SelectedMaterial, WhiteBoardPointX, WhiteBoardPointY);
                };

                AddElement(WhiteBoard);
                WhiteBoardRectangles[x, y] = WhiteBoard;
            }
        }
    }

    void CreateMaterials()
    {
        Materials = new List<Material>()
        {
            new Material()
            {
                Id = "HorizontalStick",
                Image = "images/PhMinigame/edited/HorizontalStick.png",
                HintImageUrl = "images/PhMinigame/edited/HorizontalStick_hint.png",
                Height = 80,
                Width = 190,
                CorrectX = 2,
                CorrectY = 3,
                PlaceHolderX = 1430,
                PlaceHolderY = 680,
            },
            new Material()
            {
                Id = "HorizontalStick2",
                Image = "images/PhMinigame/edited/HorizontalStick.png",
                HintImageUrl = "images/PhMinigame/edited/HorizontalStick_hint.png",
                Height = 80,
                Width = 190,
                CorrectX = 2,
                CorrectY = 1,
                PlaceHolderX = 1430,
                PlaceHolderY = 680,
            },
            new Material()
            {
                Id = "VerticalStick",
                Image = "images/PhMinigame/edited/VerticalStick.png",
                HintImageUrl = "images/PhMinigame/edited/VerticalStick_hint.png",
                Height = 100,
                Width = 190,
                CorrectX = 1,
                CorrectY = 4,
                PlaceHolderX = 1430,
                PlaceHolderY = 820,
            },
            new Material()
            {
                Id = "VerticalStick2",
                Image = "images/PhMinigame/edited/VerticalStick.png",
                HintImageUrl = "images/PhMinigame/edited/VerticalStick_hint.png",
                Height = 100,
                Width = 190,
                CorrectX = 3,
                CorrectY = 2,
                PlaceHolderX = 1430,
                PlaceHolderY = 820,
            },
            new Material()
            {
                Id = "Potentiometer",
                Image = "images/PhMinigame/edited/Potentiometer.png",
                HintImageUrl = "images/PhMinigame/edited/Potentiometer_hint.png",
                Height = 170,
                Width = 190,
                CorrectX = 3,
                CorrectY = 1,
                PlaceHolderX = 1420,
                PlaceHolderY = 30,
            },
            new Material()
            {
                Id = "OpenSwitch",
                Image = "images/PhMinigame/edited/OpenSwitch.png",
                HintImageUrl = "images/PhMinigame/edited/OpenSwitch_hint.png",
                Height = 170,
                Width = 190,
                CorrectX = 3,
                CorrectY = 3,
                PlaceHolderX = 1425,
                PlaceHolderY = 245,
            },
            new Material()
            {
                Id = "OpenSwitch2",
                Image = "images/PhMinigame/edited/OpenSwitch.png",
                HintImageUrl = "images/PhMinigame/edited/OpenSwitch_hint.png",
                Height = 170,
                Width = 190,
                CorrectX = 1,
                CorrectY = 3,
                PlaceHolderX = 1430,
                PlaceHolderY = 460,
            },
            new Material()
            {
                Id = "HorizontalLamp",
                Image = "images/PhMinigame/edited/HorizontalLamp.png",
                HintImageUrl = "images/PhMinigame/edited/HorizontalLamp_hint.png",
                Height = 80,
                Width = 190,
                CorrectX = 1,
                CorrectY = 5,
                PlaceHolderX = 1430,
                PlaceHolderY = 940,
            },
            new Material()
            {
                Id = "Battery",
                Image = "images/PhMinigame/edited/Battery.png",
                HintImageUrl = "images/PhMinigame/edited/Battery_hint.png",
                Height = 170,
                Width = 190,
                CorrectX = 1,
                CorrectY = 1,
                PlaceHolderX = 1190,
                PlaceHolderY = 78,
            },
        };

        foreach (var material in Materials)
        {
            AddMaterialToGame(material);
        }
    }

    void AddMaterialToGame(Material material)
    {
        material.Game = this;
        material.Visible = false;

        material.OnClick = (args) => OnMaterialClick(material, args);

        material.PlaceHolder = new Rectangle()
        {
            X = material.PlaceHolderX,
            Y = material.PlaceHolderY,
            Width = material.Width.Value,
            Height = material.Height.Value,
            Fill = "rgba(255,0,0,.50)",
            OnClick = (args) => OnMaterialClick(material, args)
        };

        material.HintImage = new SVGImage()
        {
            Width = material.Width,
            Height = material.Height,
            Image = material.HintImageUrl,
            OnClick = (args) => OnHintClick(material, args),
            Visible = false
        };

        AddElement(material.PlaceHolder);
        AddElement(material.HintImage);
        AddElement(material);
    }

    void OnHintClick(Material material, EventArgs args)
    {
        material.CurrentX = material.CorrectX;
        material.CurrentY = material.CorrectY;

        PlaceImageToWhiteBoardCenter(material, WhiteBoardRectangles[material.CorrectX, material.CorrectY]);
        CheckIsFinished();
    }

    void OnMaterialClick(Material material, EventArgs args)
    {
        material.HintImage.Visible = true;
        material.PlaceHolder.Visible = false;

        PlaceImageToWhiteBoardCenter(material.HintImage, WhiteBoardRectangles[material.CorrectX, material.CorrectY]);

        if (SelectedMaterial != null && SelectedMaterial != material)
        {
            HideHint(SelectedMaterial);
        }

        SelectedMaterial = material;
    }

    void HideHint(Material material)
    {
        material.HintImage.Visible = false;
        Update();
    }

    void ResetMaterial(Material material)
    {
        material.PlaceHolder.Visible = true;
        material.Visible = false;
        material.CurrentX = -1;
        material.CurrentY = -1;

        HideHint(material);
    }

    void PlaceMaterialTo(Material material, int x, int y)
    {
        PlaceImageToWhiteBoardCenter(material, WhiteBoardRectangles[x, y]);
        material.CurrentX = x;
        material.CurrentY = y;
    }

    public void PlaceImageToWhiteBoardCenter(SVGImage image, Rectangle whiteBoard)
    {
        image.X = whiteBoard.X + whiteBoard.Width / 2 - image.Width / 2;
        image.Y = whiteBoard.Y + whiteBoard.Height;

        image.Visible = true;

        Update();
    }
    public void CheckIsFinished()
    {
        bool isFinished = true;

        foreach (var material in Materials)
        {

            if (material.CurrentX != material.CorrectX || material.CurrentY != material.CorrectY)
            {

                isFinished = false;
                break;
            }
        }

        if (isFinished)
        {
            Finish(true);
        }
    }
}