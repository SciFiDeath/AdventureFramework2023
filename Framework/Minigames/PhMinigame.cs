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
       */
        
       for (int i = 0; i < (140* 6); i += 140)
        {
            for (int j = 0; j < (135* 8); j += 135)
            {
                WhiteBoard = new Rectangle ()
                {
                    X = i + 450,
                    Y = j + 50,
                    Width = 140,
                    Height = 135,
                    Fill = "transparent",
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
    */
    public void ClickWhiteBoard()
    {
        Console.WriteLine("white board clicked");
        ClickedWhiteBoards.Add(SelectedWhiteBoard);
        //ClickedWhiteBoards.Last().X = SelectedWhiteBoard.X;
        //ClickedWhiteBoards.Last().Y = SelectedWhiteBoard.Y;
        SelectedElement.X = SelectedWhiteBoard.X;
        SelectedElement.Y = SelectedWhiteBoard.Y;
        Update();
    }  
  

}