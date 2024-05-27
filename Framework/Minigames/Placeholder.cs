using System.Data;

namespace Framework.Minigames.MinigameDefClasses;

public class MinigameTut : MinigameDefBase
{
    public override string BackgroundImage { get; set; } = "images/IMG_2457.JPG";

    [Element]
    public Rectangle Rect { get; set; }
    public MinigameTut()
    {

        Rect = new()
        {
            Id = "Box",
            X = 1500,
            Y = 0,
            Width = 300,
            Height = 250,
            Fill = "red"
            
        };
        var right = new Rectangle()
        {
            X = 1818,
            Y = 40,
            Width = 100,
            Height = 1000,
            Fill = "#ffff9380",
            OnClick = (args) => nextpage(args)
        };
        AddElement(right);
        var Rect1 = new Rectangle()
        {
            X = 1400,
            Y = 330,
            Width = 100,
            Height = 200,
            Fill = "red"
        };
        AddElement(Rect1);
        var Rect2 = new Rectangle()
        {
            X = 1230,
            Y = 400,
            Width = 70,
            Height = 150,
            Fill = "red"
        };
        AddElement(Rect2);
        var Rect3 = new Rectangle()
        {
            X = 1400,
            Y = 330,
            Width = 100,
            Height = 200,
            Fill = "red"
        };
        AddElement(Rect3);
    }
    private void nextpage(EventArgs e)
    {
        BackgroundImage = "images/IMG_2455.jpg";
        Update();
    }
}

