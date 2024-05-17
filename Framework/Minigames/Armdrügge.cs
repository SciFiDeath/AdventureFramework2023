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
        int enemycounter = 0;
        int clickcount = 0; // Wie oft man auf den Kreis gedrückt hat (für die Farben zustädnig)
        int Ycord = 758; //Unterster Startpunkt
        int enemyYcord = 758;


        AddElement(
                  new Rectangle()
                  {
                      // Progressbar links (Spieler)
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
                      //Progressbar rechts (Gegner)
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
                      OnClick = (args) => { progressClick(Ycord, clickcount); Ycord = Ycord - 107; clickcount++; }, //OnClick wird Funktion ausgeführt, die die Füllung macht und Ycord wird angepasst, damit es hoch geht
                  }
              );
        while (enemycounter < 6)
        {
            // Solange enemyounter unter 6 ist (6, weil wir 6 Farben haben von Weiss bis und mit Rot)
            enemyclick(enemyYcord, enemycounter);
            enemycounter++;
            enemyYcord = enemyYcord - 107;
            Update();

        }
        Update();
    }

    public void enemyclick(int Ycord, int enemycounter) //Funktion, die für das Auffüllen der Gegnerischen Progressbar zuständig ist
    {
        List<string> colors = new List<string> { "LightGoldenrodYellow", "yellow", "Gold", "orange", "DarkOrange", "red" };
        AddElement(
               new Rectangle()
               {
                   X = 1445,
                   Y = Ycord,
                   Width = 10,
                   Height = 21,
                   Fill = "transparent",
                   Stroke = colors[enemycounter],
                   StrokeWidth = 100
               }
           );
        Update();
    }

    public void progressClick(int Ycord, int counter)
    {
        List<string> colors = new List<string> { "LightGoldenrodYellow", "yellow", "Gold", "orange", "DarkOrange", "red" };
        //Stroke also Füllfarbe wird mit der Liste berechnet, für jeden Klick erhöht sich counter und somit verändert sich die Farbe
        if (counter < colors.Count)
        {
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
        }
        Update();
    }

}


