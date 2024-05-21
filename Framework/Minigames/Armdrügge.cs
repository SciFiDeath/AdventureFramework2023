using System.Data;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Framework.Minigames.MinigameDefClasses;

public class MyMinigame6 : MinigameDefBase
{
    public override string BackgroundImage { get; set; } = "/images/Armdrücken test.jpg";

    int enemycounter = 0;
    int clickcount = 0; // Wie oft man auf den Kreis gedrückt hat (für die Farben zustädnig)
    int Ycord = 758; //Unterster Startpunkt
    int enemyYcord = 758;
    int score = 0;

    GameObjectContainer<Rectangle> Quadrate { get; set; } = new();

    public MyMinigame6()
    {

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
                  OnClick = (args) =>
                  {
                      progressClick(Ycord, clickcount);
                      Ycord = Ycord - 107;
                      clickcount++;
                      imageswap();
                      Console.WriteLine(Elements);
                  }, //OnClick wird Funktion ausgeführt, die die Füllung macht und Ycord wird angepasst, damit es hoch geht
              }
          );
        Update();



        _ = StartEnemyClick();
        Update();
    }

    public void imageswap()
    {


        if (clickcount == 6)
        {

            Quadrate.KillAll();

            score++;
            enemyYcord = 758;
            enemycounter = 0;
            clickcount = 0;
            Ycord = 758;
        }
        else if (enemycounter == 6)
        {
            Quadrate.KillAll();

            score--;
            enemyYcord = 758;
            enemycounter = 0;
            clickcount = 0;
            Ycord = 758;
        }
        if (score == 0)
        {
            BackgroundImage = "/images/Armdrücken test.jpg";
            Update();
        }
        else if (score == 1)
        {
            BackgroundImage = "/images/calculator.png";
            Update();
        }
        else if (score == -1)
        {
            BackgroundImage = "/images/HM3_hallwayN.jpg";
            Update();
        }
        Update();
    }

    public async Task StartEnemyClick()
    {
        using PeriodicTimer timer = new(TimeSpan.FromMilliseconds(1000));

        while (enemycounter < 6 && await timer.WaitForNextTickAsync())
        {
            List<string> colors = new List<string> { "LightGoldenrodYellow", "yellow", "Gold", "orange", "DarkOrange", "red" };
            var x = new Rectangle()
            {
                X = 1445,
                Y = enemyYcord,
                Width = 50,
                Height = 21,
                Fill = colors[enemycounter],
                //  Stroke = colors[enemycounter],
                // StrokeWidth = 100
            };

            AddElement(x);
            Quadrate.Add(x);

            enemycounter++;
            enemyYcord = enemyYcord - 107;

            imageswap();

            Update();
        }
    }

    public void progressClick(int Ycord, int counter)
    {
        List<string> colors = new List<string> { "LightGoldenrodYellow", "yellow", "Gold", "orange", "DarkOrange", "red" };
        //Stroke also Füllfarbe wird mit der Liste berechnet, für jeden Klick erhöht sich counter und somit verändert sich die Farbe
        if (counter < colors.Count)
        {
            var x = new Rectangle()
            {
                X = 150,
                Y = Ycord,
                Width = 50,
                Height = 21,
                Fill = colors[counter],
                //Stroke = colors[counter],
                // StrokeWidth = 100
            };

            AddElement(x);
            Quadrate.Add(x);
        }
        Update();
    }

}