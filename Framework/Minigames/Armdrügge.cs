using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace Framework.Minigames.MinigameDefClasses;


public class MyMinigame6 : MinigameDefBase
{


    public override string BackgroundImage { get; set; } = "/images/Armdrücken test.jpg";

    [Element]
    public Rectangle Recttop { get; set; }
    [Element]
    public Rectangle Rectleft { get; set;}
    [Element]
    public Rectangle Rectbottom { get; set;}
    [Element]
    public Rectangle Rectright { get; set;}
    [Element]
    public Rectangle Rectfill { get; set;}

    public GameObjectContainer<Rectangle> RectRot {get; set;} = new();


    public GameObjectContainer<Rectangle> Rects {get; set;} = new();
    public MyMinigame6()
    {
        Rectleft = new()
        {
            Id = "leftrectb",
            X = 150,
            Y = 150,
            Width = 20,
            Height = 700,
            Fill = "black",
            // OnClick = spawnRect,
            

        
        };
   
        Recttop = new()
        {
            Id = "toprectb",
            X = 150,
            Y = 150,
            Width = 100,
            Height = 20,
            Fill = "black",
        };

        Rectright = new()
        {
            Id = "rightrectb",
            X = 249,
            Y = 150,
            Width = 20,
            Height = 700,
            Fill = "black",
        };

        Rectbottom= new()
        {
            Id = "bottomrectb",
            X = 150,
            Y = 830,
            Width = 100,
            Height = 20,
            Fill = "black",
        };
        Rects.Add(Rectbottom);
        Rects.Add(Rectright);
        Rects.Add(Recttop);
        Rects.Add(Rectleft);
        Update();
    }


 public void spawnRect(EventArgs args)
    {

    Random rnd = new Random();

    int randomx = rnd.Next(1,1900);
    int randomy = rnd.Next(1,1080);
        
            var rect = new Rectangle()
            {
                Id = "rectr",
                X = randomx,
                Y = randomy,
                Width = 50,
                Height= 50,
                Fill = "red",
                OnClick = (args) => {RectRot.KillId("rectr"); Update();},
                
                
                
            };
            AddElement(rect);
            RectRot.Add(rect);
        
        Update();
    }
   
}


