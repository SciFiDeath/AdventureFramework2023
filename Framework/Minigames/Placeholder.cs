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
        Error0 = new()
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
        Error1 = new()
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
    //public void spotted(EventArgs e, int? x, int? y, int width, int height, Rectangle rect)
    //{
    //    rect.Visible = false;
    //    rectspot = new Rectangle()
    //    {
    //        X = x,
    //        Y = y,
    //        Width = width,
    //        Height = height,
    //        Fill = "red"
    //    };
    //    AddElement(rectspot);
    //}

    public void ChangeColor(EventArgs e, Rectangle rect)
    {
        rect.Fill = "rgba(0,255,0,.5)";
        Update();
        errorsspotted++;
        endgame();
    }
    public void endgame() 
    {
        if (errorsspotted == 2) 
        {
            Finish(null, "test");
        }
    }
    //public string? color(bool i)
    //{
    //    if (i == false)
    //    {
    //        return "red";
    //    }
    //    else { return "white"; }
    //    Update();
    //}
}
////    public void foundfun (EventArgs e, bool i)
////    {
////        i = true;
////    }
////}
