using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace Framework.Minigames.MinigameDefClasses;


public class MyMinigame6 : MinigameDefBase
{


    public override string BackgroundImage { get; set; } = "/images/HM3_hallwayN.jpg";

    [Element]
    public Rectangle Rect { get; set; }


    public GameObjectContainer<Rectangle> Rects {get; set;} = new();
    public MyMinigame6()
    {
        Rect = new()
        {
            Id = "rect",
            X = 100,
            Y = 100,
            Width = 100,
            Height = 100,
            Fill = "green",
            OnClick = spawnRect,

        
        };
            Rects.Add(Rect);


 Update();
    }


 public void spawnRect(EventArgs args)
    {

    Random rnd = new Random();

    int randomx = rnd.Next(1,1900);
    int randomy = rnd.Next(1,1080);
        
            var rect = new Rectangle()
            {
                X = randomx,
                Y = randomy,
                Width = 50,
                Height= 50,
                Fill = "red",
                OnClick = tot,
            };
            AddElement(rect);
            Rects.Add(rect);
        
        Update();
    }
   public void tot(EventArgs args)
   {
    Rect.Kill();
   }
}


