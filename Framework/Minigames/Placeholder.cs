using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace Framework.Minigames.MinigameDefClasses;

public class MinigameTut : MinigameDefBase
{
    public int errorsspotted { get; set; } = 0;
    public override string BackgroundImage { get; set; } = "images/IMG_2455.JPG";
    [Element]
    public Rectangle Rects { get; set; }
    public Rectangle newRect { get; set; }

    [Element] public Rectangle Error0 { get; set; }
    [Element] public Rectangle Error1 { get; set; }
    [Element] public Rectangle Error2 { get; set; }
    [Element] public Rectangle Error3 { get; set; }
    [Element] public Rectangle Error4 { get; set; }
    [Element] public Rectangle Error5 { get; set; }
    [Element] public Rectangle Error6 { get; set; }
    [Element] public Rectangle Error7 { get; set; }
    [Element] public Rectangle Error8 { get; set; }
    [Element] public Rectangle Error9 { get; set; }

    public GameObjectContainer<Rectangle> Errors { get; set; } = new();

    public MinigameTut()
    {

        Rects = new()
        {
            X = 1818,
            Y = 200,
            Width = 100,
            Height = 600,
            Fill = "red",
            OnClick = (args) => errorspage(args)
        };
    }
    public void errorspage(EventArgs e)
    {
        BackgroundImage = "images/IMG_2457.jpg";
        Update();
        newRect = new()
        {
            X = 0,
            Y = 200,
            Width = 100,
            Height = 600,
            Fill = "red",
            OnClick = (args) => originalpage(args)
        };
        AddElement(newRect);
        Rects.Kill();
        Error0 = new()
        {
            X = 1280,
            Y = 10,
            Width = 250,
            Height = 250,
            Fill = "transperant",
            OnClick = (args) => spotted(Error0.X, Error0.Y, Error0.Width, Error0.Height)

        };
        AddElement(Error0);

        foreach (var error in Errors)
        {
            error.Value.OnClick = (args) => Elements.KillId(error.key);
        }

        Update();
    }
    public void originalpage(EventArgs e)
    {
        BackgroundImage = "images/IMG_2455.jpg";
        Update();
        Rects = new()
        {
            X = 1818,
            Y = 200,
            Width = 100,
            Height = 600,
            Fill = "red",
            OnClick = (args) => errorspage(args)
        };
        AddElement(Rects);
        newRect.Kill();
        Error0.Kill();
    }
    public void spotted(int? x, int? y, int width, int height)
    {

        var rect = new Rectangle()
        { 
            X = x, 
            Y = y,
            Width = width,
            Height = height,
            Fill = "red"
        }; 
    }
    
}
