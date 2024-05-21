using System.Data;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Framework.Minigames.MinigameDefClasses;

public class MyMinigame6 : MinigameDefBase
{
    public override string BackgroundImage { get; set; } = "/images/Arm_1.png";

    int enemycounter = 0;
    int clickcount = 0; // Wie oft man auf den Kreis gedrückt hat (für die Farben zustädnig)
    int Ycord = 758; //Unterster Startpunkt
    int enemyYcord = 758;
    int score = 0;

    int circleX = 1200;

    int circleY = 500;

    bool gameover = false;

    string redcol = "red";
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


        circlechanger();
        Update();



        _ = StartEnemyClick();
        Update();
    }

    public void circlechanger()
    {
        AddElement(
      new Circle()
      {
          //Hand zum klicken
          Id = "Circle1",
          R = 60,
          CX = circleX,
          CY = circleY,
          Fill = redcol,
          Stroke = redcol,
          StrokeWidth = 40,
          OnClick = (args) =>
          {
              progressClick(Ycord, clickcount);
              Ycord = Ycord - 107;
              clickcount++;
              imageswap();
              Console.WriteLine(Elements);
              Elements.KillId("Circle1");
              circlechanger();
          }, //OnClick wird Funktion ausgeführt, die die Füllung macht und Ycord wird angepasst, damit es hoch geht
      }
  );
        Update();
    }


    public void imageswap()
    {


        if (clickcount == 7)
        {

            Quadrate.KillAll();

            score++;
            enemyYcord = 758;
            enemycounter = 0;
            clickcount = 0;
            Ycord = 758;
        }
        else if (enemycounter == 7)
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
            BackgroundImage = "/images/Arm_1.png";
            circleX = 1200;
            circleY = 500;
            Update();
            Elements.KillId("Circle1");
            circlechanger();
        }
        else if (score == 1)
        {
            BackgroundImage = "/images/Arm_4.png";
            circleX = 970;
            circleY = 720;
            Update();
            Elements.KillId("Circle1");
            circlechanger();
        }
        else if (score == -1)
        {
            BackgroundImage = "/images/Arm_2_.png";
            circleX = 1300;
            circleY = 500;
            Update();
            Elements.KillId("Circle1");
            circlechanger();
        }
        else if (score == -2)
        {
            Elements.KillId("Circle1");
            BackgroundImage = "/images/Arm_3.png";
            gameover = true;
            Update();
        }
        else if (score == 2)
        {

            BackgroundImage = "/images/Arm_5.png";
            redcol = "transparent";
            gameover = true;
            Update();
        }

        Update();
    }

    public async Task StartEnemyClick()
    {
        using PeriodicTimer timer = new(TimeSpan.FromMilliseconds(500));

        while (enemycounter < 7 && await timer.WaitForNextTickAsync() && gameover == false)
        {
            List<string> colors = new List<string> { "LightGoldenrodYellow", "yellow", "Gold", "orange", "DarkOrange", "red", "red" };
            var x = new Rectangle()
            {
                X = 1445,
                Y = enemyYcord,
                Width = 150,
                Height = 21,
                Fill = colors[enemycounter],

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
        List<string> colors = new List<string> { "LightGoldenrodYellow", "yellow", "Gold", "orange", "DarkOrange", "red", "red" };
        //Stroke also Füllfarbe wird mit der Liste berechnet, für jeden Klick erhöht sich counter und somit verändert sich die Farbe
        if (counter < colors.Count && gameover == false)
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