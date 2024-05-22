
/*using Framework.Mouse;

namespace Framework.Minigames.MinigameDefClasses;

public class PhMinigame : MinigameDefBase
{
    public override string BackgroundImage { get; set; } = "images/PhMinigame/platform2.jpg";

    static List<Rectangle> ClickedWhiteBoards = new List<Rectangle>();
    public SVGImage YellowMaterial { get; set; }
    public SVGImage BlueMaterial { get; set; }
    public SVGImage PinkMaterial { get; set; }
    public SVGImage GreenMaterial { get; set; }
    public Rectangle WhiteBoard { get; set; }
    public SVGImage SelectedElement { get; set; }
    public SVGImage SelectedWhiteBoard { get; set; }

    public PhMinigame()
    {
        YellowMaterial = new()
        {
            X = 1500,
            Y = 200,
            Width = 140,
            Height = 135,
            Image = "images/PhMinigame/m1.jpg",
            OnClick = ClickYellow
        };
        AddElement(YellowMaterial);

        BlueMaterial = new()
        {
            X = 1500,
            Y = 400,
            Width = 140,
            Height = 135,
            Image = "images/PhMinigame/m4.jpg",
            OnClick = ClickBlue
        };
        AddElement(BlueMaterial);

        PinkMaterial = new()
        {
            X = 1500,
            Y = 600,
            Width = 140,
            Height = 135,
            Image = "images/PhMinigame/m2.jpg",
            OnClick = ClickPink
        };
        AddElement(PinkMaterial);

        GreenMaterial = new()
        {
            X = 1500,
            Y = 800,
            Width = 140,
            Height = 135,
            Image = "images/PhMinigame/m3.jpg",
            OnClick = ClickGreen
        };
        AddElement(GreenMaterial);

        /*
        WhiteBoard = new Rectangle()
        {
            X = 450,
            Y = 50,
            Width = 140,
            Height = 135,
            Fill = "blue",
            ZIndex = -2,
            OnClick = MoveMaterial
        };
        AddElement(WhiteBoard);
       
 
       
        
        
       for (int i = 0; i < (140* 6); i += 140)
        {
            for (int j = 0; j < (135* 8); j += 135)
            {
                WhiteBoard = new Rectangle ()
                {
                    X = i + 450,
                    Y = j + 55,
                    Width = 140,
                    Height = 135,
                    Fill = "rgba(0,0,0,.3)",
                    ZIndex = -2,
                    OnClick = ClickWhiteBoard
                };
                AddElement(WhiteBoard);
                
            }

        }
      

    }

    
    public void ClickYellow(EventArgs args)
    {
        Console.WriteLine("Hello World!");
        SelectedElement = YellowMaterial;
    }
    public void ClickBlue(EventArgs args)
    {
        Console.WriteLine("Hello World!");
        SelectedElement = BlueMaterial;
    }
    public void ClickPink(EventArgs args)
    {
        Console.WriteLine("Hello World!");
        SelectedElement = PinkMaterial;
    }
    public void ClickGreen(EventArgs args)
    {
        Console.WriteLine("Hello World!");
        SelectedElement = GreenMaterial;
    }
    /*
    public void MoveMaterial()
    {
        SelectedElement.X = SelectedWhiteBoard.X;
        SelectedElement.Y = SelectedWhiteBoard.Y;
        //WhiteBoard.Image = SelectedElement.Image;
        Update();
    } 
    
    public void ClickWhiteBoard(EventArgs args)
    {
        Console.WriteLine("white board clicked");

        





        //ClickedWhiteBoards.Add(SelectedWhiteBoard);
        //ClickedWhiteBoards.Last().X = SelectedWhiteBoard.X;
        //ClickedWhiteBoards.Last().Y = SelectedWhiteBoard.Y;
        //SelectedElement.X = SelectedWhiteBoard.X;
        //SelectedElement.Y = SelectedWhiteBoard.Y;
        Update();
    }  
  

}

*/

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