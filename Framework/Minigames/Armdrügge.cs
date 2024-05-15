using System.Data;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Framework.Minigames.MinigameDefClasses;


public class MyMinigame6 : MinigameDefBase
{


    public override string BackgroundImage { get; set; } = "/images/Armdrücken test.jpg";

    [Element]
    public Rectangle Recttop { get; set; }
    [Element]
    public Rectangle Rectleft { get; set; }
    [Element]
    public Rectangle Rectbottom { get; set; }
    [Element]
    public Rectangle Rectright { get; set; }
    [Element]
    public Rectangle Rectfill { get; set; }

    public Rectangle Progressbar { get; set; }

    public GameObjectContainer<Rectangle> RectRot { get; set; } = new();


    public GameObjectContainer<Rectangle> Rects { get; set; } = new();


    public MyMinigame6()
    {

        int clickcount = 0;
        int Ycord = 758;

        AddElement(
                  new Rectangle()
                  {
                      // Progressbar links
                      X = 80,
                      Y = 150,
                      Width = 150,
                      Height = 700,
                      Fill = "transparent",
                      Stroke = "black",
                      StrokeWidth = 40,
                  }
              );

        AddElement(
                  new Rectangle()
                  {
                      //Progressbar rechts
                      X = 1375,
                      Y = 150,
                      Width = 150,
                      Height = 700,
                      Fill = "transparent",
                      Stroke = "black",
                      StrokeWidth = 40,
                  }
              );

        AddElement(
                  new Circle()
                  {
                      //Hand zum klicken
                      R = 100,
                      CX = 550,
                      CY = 500,
                      Fill = "orange",
                      Stroke = "red",
                      StrokeWidth = 40,
                      OnClick = (args) => { progressClick(Ycord, clickcount); Ycord = Ycord - 107; clickcount++; },
                  }

              );

        Update();
    }


    public void progressClick(int Ycord, int counter)
    {
        List<string> colors = new List<string> { "LightGoldenrodYellow", "yellow", "Gold", "orange", "DarkOrange", "red" };



        AddElement(
                   new Rectangle()
                   {
                       X = 150,
                       Y = Ycord,
                       Width = 10,
                       Height = 21,
                       Fill = "transparent",
                       Stroke = colors[counter],
                       StrokeWidth = 100
                   }
               );

        Update();
    }

}


