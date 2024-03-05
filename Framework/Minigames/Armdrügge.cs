using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace Framework.Minigames.MinigameDefClasses;

public class MyMinigame6 : MinigameDefBase
{
    public override string BackgroundImage { get; set; } = "/images/HM3_hallwayN.jpg";

    [Element]
    public Rectangle Rect { get; set; }



    public MyMinigame6()
    {
        Rect = new()
        {
            Id = "rect1",
            X = 100,
            Y = 100,
            Width = 100,
            Height = 100,
            Fill = "green",
            OnClick = spawnRect,

        
        };


 Update();
    }

public void hello(EventArgs args)
{
    Console.WriteLine("Hello");
}
 public void spawnRect(EventArgs args)
    {
        for (int i = 100; i < 1900; i+= 100)
        {
            var rect = new Rectangle()
            {
                X = i,
                Y = 100,
                Width = 50,
                Height= 50,
                Fill = "red",
                OnClick = tot,
            };
            AddElement(rect);
        }
        Update();
    }
   public void tot(EventArgs args)
   {
    Rect.Kill();
   }
}


