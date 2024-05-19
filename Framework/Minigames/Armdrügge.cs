using System.Data;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Framework.Minigames.MinigameDefClasses;

public class MyMinigame6 : MinigameDefBase
{
    public override string BackgroundImage { get; set; } = "/images/Armdrücken test.jpg";

    public MyMinigame6()
    {
        int enemycounter = 0;
        int clickcount = 0; // Wie oft man auf den Kreis gedrückt hat (für die Farben zustädnig)
        int Ycord = 758; //Unterster Startpunkt
        int enemyYcord = 758;
        int score = 0;

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
                      imageswap(ref clickcount, ref enemycounter, ref Ycord, ref enemyYcord, ref score);
                  }, //OnClick wird Funktion ausgeführt, die die Füllung macht und Ycord wird angepasst, damit es hoch geht
              }
          );
        Update();

        EnemyState enemyState = new EnemyState
        {
            Ycord = enemyYcord,
            EnemyCounter = enemycounter,
            EnemyYcord = enemyYcord
        };

        _ = StartEnemyClick(enemyState);
        Update();
    }

    public void imageswap(ref int yourscore, ref int enemyscore, ref int Ycord, ref int enemyYcord, ref int gamescore)
    {


        if (yourscore == 6)
        {
            gamescore++;
            yourscore = 0;
            Ycord = 758;
        }
        else if (enemyscore == 5)
        {
            gamescore--;
            enemyscore = 0;
            enemyYcord = 758;
        }
        if (gamescore == 0)
        {
            BackgroundImage = "/images/Armdrücken test.jpg";
            Update();
        }
        else if (gamescore == 1)
        {
            BackgroundImage = "/images/calculator.png";
            Update();
        }
        else if (gamescore == -1)
        {
            BackgroundImage = "/images/HM3_hallwayN.jpg";
            Update();
        }
        Update();
    }

    public async Task StartEnemyClick(EnemyState enemyState)
    {
        using PeriodicTimer timer = new(TimeSpan.FromMilliseconds(1000));

        while (enemyState.EnemyCounter < 6 && await timer.WaitForNextTickAsync())
        {
            List<string> colors = new List<string> { "LightGoldenrodYellow", "yellow", "Gold", "orange", "DarkOrange", "red" };
            AddElement(
                   new Rectangle()
                   {
                       X = 1445,
                       Y = enemyState.EnemyYcord,
                       Width = 10,
                       Height = 21,
                       Fill = "transparent",
                       Stroke = colors[enemyState.EnemyCounter],
                       StrokeWidth = 100
                   }
               );

            enemyState.EnemyCounter++;
            enemyState.EnemyYcord = enemyState.EnemyYcord - 107;

            Update();
        }
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

    public class EnemyState
    {
        public int Ycord { get; set; }
        public int EnemyCounter { get; set; }
        public int EnemyYcord { get; set; }
    }
}
