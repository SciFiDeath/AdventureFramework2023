using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace Framework.Minigames.MinigameDefClasses;

public class MinigameTut : MinigameDefBase
{
    public int errorsspotted = 0;
    public override string BackgroundImage { get; set; } = "images/IMG_2455.JPG";
    [Element]
    public Rectangle Rects { get; set; }
    public Rectangle newRect { get; set; }
    public Rectangle rectspot { get; set; }
    public GameObjectContainer<Rectangle> Errors { get; set; } = new();
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
        bool found = false;
        Error0 = new() //Box
        {
            X = 1200,
            Y = 10,
            Width = 350,
            Height = 250,
            Fill = "transparent",
            OnClick = (args) => ChangeColor(args, Error0)

        };
        Errors.Add(Error0);
        AddElement(Error0);
        Update();
        Error1 = new() //blue zylinder
        {
            X = 1000,
            Y = 350,
            Width = 110,
            Height = 240,
            Fill = "transparent",
            OnClick = (args) => ChangeColor(args, Error1)

        };
        Errors.Add(Error1);
        AddElement(Error1);
        Update();
        Error2 = new() //purple strip
        {
            X = 1450,
            Y = 360,
            Width = 110,
            Height = 200,
            Fill = "transparent",
            OnClick = (args) => ChangeColor(args, Error2)

        };
        Errors.Add(Error2);
        AddElement(Error2);
        Update();
        Error3 = new()//spoons
        {
            X = 1380,
            Y = 850,
            Width = 150,
            Height = 250,
            Fill = "transparent",
            OnClick = (args) => ChangeColor(args, Error3)

        };
        Errors.Add(Error3);
        AddElement(Error3);
        Update();
        Error4 = new() // black lid
        {
            X = 1260,
            Y = 510,
            Width = 120,
            Height = 200,
            Fill = "transparent",
            OnClick = (args) => ChangeColor(args, Error4)

        };
        Errors.Add(Error4);
        AddElement(Error4);
        Update();
        Error5 = new()//B12 Box upside down
        {
            X = 1180,
            Y = 620,
            Width = 55,
            Height = 100,
            Fill = "transparent",
            OnClick = (args) => ChangeColor(args, Error5)

        };
        Errors.Add(Error5);
        AddElement(Error5);
        Update();
        Error6 = new() //blue lid upside down
        {
            X = 792,
            Y = 390,
            Width = 100,
            Height = 180,
            Fill = "transparent",
            OnClick = (args) => ChangeColor(args, Error6)

        };
        Errors.Add(Error6);
        AddElement(Error6);
        Update();
        Error7 = new() //türkis lid moved
        {
            X = 750,
            Y = 600,
            Width = 100,
            Height = 140,
            Fill = "transparent",
            OnClick = (args) => ChangeColor(args, Error7)

        };
        Errors.Add(Error7);
        AddElement(Error7);
        Update();
        Error8 = new() //cutting board
        {
            X = 750,
            Y = 770,
            Width = 100,
            Height = 300,
            Fill = "transparent",
            OnClick = (args) => ChangeColor(args, Error8)

        };
        Errors.Add(Error8);
        AddElement(Error8);
        Update();
        Error9 = new()//missing 123
        {
            X = 560,
            Y = 630,
            Width = 75,
            Height = 100,
            Fill = "transparent",
            OnClick = (args) => ChangeColor(args, Error9)

        };
        Errors.Add(Error9);
        AddElement(Error9);
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
        Update();
        //Error0.Kill();
    }
    public void ChangeColor(EventArgs e, Rectangle rect)
    {
        rect.Fill = "rgba(0,255,0,.5)";
        Update();
        errorsspotted++;
        if (errorsspotted == 10)
        {
            Finish(null, "IMG_2455");
        }
    }
}
