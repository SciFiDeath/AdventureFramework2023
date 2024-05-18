namespace Framework.Minigames.MinigameDefClasses;

public class PhMinigame : MinigameDefBase
{
    public override string BackgroundImage { get; set; } = "images/PhMinigame/stromkreis.jpg";

    public SVGImage RectRed { get; set; }

    public Rectangle RectYellow { get; set; }

    public SVGImage SelectedElement { get; set; }

    public PhMinigame()
    {
        RectRed = new()
        {
            X = 1350,
            Y = 300,
            Width = 100,
            Height = 100,
            Image = "images/PhMinigame/1.jpg",
            OnClick = ClickRed
            //OnClick = (args) => { Console.WriteLine("Hello World!"); }
        };

        RectYellow = new()
        {
            X = 700,
            Y = 500,
            Width = 100,
            Height = 100,
            Fill = "transparent",
            ZIndex = -1,
            OnClick = MoveRed
        };


        AddElement(RectRed);
        AddElement(RectYellow);
    }


    public void ClickRed(EventArgs args)
    {
        Console.WriteLine("Hello World!");
        SelectedElement = RectRed;
    }

    public void MoveRed(EventArgs args)
    {
        SelectedElement.X = RectYellow.X;
        SelectedElement.Y = RectYellow.Y;
  
        Update();
    }

}