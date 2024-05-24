/*
using Framework.Mouse;

namespace Framework.Minigames.MinigameDefClasses;

public class Point 
{
    public Point(int X,int Y)
    {
        this.X = X;
        this.Y = Y;
    }
    public int  X { get; set; }
    public int Y { get; set; }
}
public class PhMinigame : MinigameDefBase
{
    public override string BackgroundImage { get; set; } = "images/PhMinigame/edited/Platform1.png";

    Rectangle BackgroundElement { get; set; }
    SVGImage YellowMaterial { get; set; }
    SVGImage BlueMaterial { get; set; }
    SVGImage PinkMaterial { get; set; }
    SVGImage GreenMaterial { get; set; }
    SVGImage? SelectedElement { get; set; }



    readonly Point YellowMaterialStartingPoint = new Point(1500, 200);
    readonly Point BlueMaterialStartingPoint = new Point(1500, 400);
    readonly Point PinkMaterialStartingPoint = new Point(1500, 600);
    readonly Point GreenMaterialStartingPoint = new Point(1500,800);

    readonly static int ColumnCount = 6;
    readonly static int RowCount = 8;
    readonly static int WhiteBoardWidth = 128;
    readonly static int WhiteBoardHeight = 105;
    Point YellowMaterialLocation = new Point(1, 1);
    Rectangle YellowMaterialWhiteBoard = null;

    public Dictionary<SVGImage, Point> PlacedMaterials = [];

    public PhMinigame()
    {
        BackgroundElement = new Rectangle()
        {
            X = 0,
            Y = 0,
            Width = 2000,
            Height = 1400,
            Fill = "transparent",
            ZIndex = -3,
            OnClick = (args) =>
            {
                if (SelectedElement == null) return;

                ResetMaterial(SelectedElement);

                SelectedElement = null;

                Update();

            },
        };

        AddElement(BackgroundElement);

        for (int x = 0; x < ColumnCount; x++)
        {
            for (int y = 0; y < RowCount; y++)
            {
                Rectangle WhiteBoard = new Rectangle()
                {
                    X = (x * WhiteBoardWidth) + 380 + 5,
                    Y = (y * WhiteBoardHeight) + 95 + 5,
                    Width = WhiteBoardWidth - 10,
                    Height = WhiteBoardHeight - 10,
                    Fill = "transparent",
                    //Fill = "rgba(0,255,0,.5)",
                    ZIndex = -2,
                };

                if (YellowMaterialLocation.X == x && YellowMaterialLocation.Y == y)
                {
                    YellowMaterialWhiteBoard = WhiteBoard;
                }

                Point WhiteBoardPoint = new(x, y);

                WhiteBoard.OnClick = (args) =>
                {
                    if (SelectedElement == null) return;

                    SelectedElement.X = WhiteBoard.X;
                    SelectedElement.Y = WhiteBoard.Y;

                    if (PlacedMaterials.ContainsKey(SelectedElement))
                    {
                        PlacedMaterials[SelectedElement] = WhiteBoardPoint;
                    }
                    else
                    {
                        PlacedMaterials.Add(SelectedElement, WhiteBoardPoint);
                    }

                    Update();
                };

                AddElement(WhiteBoard);

            }

        }

        YellowMaterial = new()
        {
            X = YellowMaterialStartingPoint.X,
            Y = YellowMaterialStartingPoint.Y,
            Width = WhiteBoardWidth,
            Height = WhiteBoardHeight,
            Image = "images/PhMinigame//edited/Potentiometer.png",
            OnClick = ClickYellow
        };
        AddElement(YellowMaterial);

        BlueMaterial = new()
        {
            X = BlueMaterialStartingPoint.X,
            Y = BlueMaterialStartingPoint.Y,
            Width = 140,
            Height = 135,
            Image = "images/PhMinigame/m4.jpg",
            OnClick = ClickBlue
        };
        AddElement(BlueMaterial);

        PinkMaterial = new()
        {
            X = PinkMaterialStartingPoint.X,
            Y = PinkMaterialStartingPoint.Y,
            Width = 140,
            Height = 135,
            Image = "images/PhMinigame/m2.jpg",
            OnClick = ClickPink
        };
        AddElement(PinkMaterial);

        GreenMaterial = new()
        {
            X = GreenMaterialStartingPoint.X,
            Y = GreenMaterialStartingPoint.Y,
            Width = 140,
            Height = 135,
            Image = "images/PhMinigame/edited/HorizontalStick.png",
            OnClick = ClickGreen
        };
        AddElement(GreenMaterial);

    }

    public void ClickYellow(EventArgs args)
    {
        SelectedElement = YellowMaterial;

        ResetWhiteBoard();

        YellowMaterialWhiteBoard.Fill = "rgba(0,255,0,.50)";

        Update();
    }
    public void ClickBlue(EventArgs args)
    {
        SelectedElement = BlueMaterial;


        ResetWhiteBoard();

        //YellowMaterialWhiteBoard.Fill = "rgba(0,255,0,.50)";

        Update();


    }
    public void ClickPink(EventArgs args)
    {
        SelectedElement = PinkMaterial;
    }
    public void ClickGreen(EventArgs args)
    {
        SelectedElement = GreenMaterial;
    }

    // Move the material to initial point and remove from placedMaterials
    public void ResetMaterial(SVGImage Material)
    {
        if (Material == YellowMaterial)
        {
            YellowMaterial.X = YellowMaterialStartingPoint.X;
            YellowMaterial.Y = YellowMaterialStartingPoint.Y;
        }

        if (Material == BlueMaterial)
        {
            BlueMaterial.X = BlueMaterialStartingPoint.X;
            BlueMaterial.Y = BlueMaterialStartingPoint.Y;
        }

        if (Material == PinkMaterial)
        {
            PinkMaterial.X = PinkMaterialStartingPoint.X;
            PinkMaterial.Y = PinkMaterialStartingPoint.Y;
        }

        if (Material == GreenMaterial)
        {
            GreenMaterial.X = GreenMaterialStartingPoint.X;
            GreenMaterial.Y = GreenMaterialStartingPoint.Y;
        }

        PlacedMaterials.Remove(Material);

    }

    public void ResetWhiteBoard()
    {
        YellowMaterialWhiteBoard.Fill = "transparent";
    }





}


*/

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
                    X = (x * WhiteBoardRectangleWidth) + 380    /* + 5*/,
                    Y = (y * WhiteBoardRectangleHeight) + 95    /* + 5*/,
                    Width = WhiteBoardRectangleWidth            /* - 10*/,
                    Height = WhiteBoardRectangleHeight          /* - 10 */,
                    Fill = "transparent",
                    //Fill = "rgba(0,255,0,.5)", //Şeffaf yeşil test için
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