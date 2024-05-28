using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Framework.Minigames.MinigameDefClasses;

public class BiologyMinigame : MinigameDefBase
{
    int score = 0;
    int lives = 3;
    int seconds = 60;
    int QuestionNumber = 1;
    public override async Task GameLoop(CancellationToken token)
    {
        while (seconds != 0)
        {
            await Task.Delay(1000);
            seconds = seconds - 1;
            token.ThrowIfCancellationRequested();
        }
    }
    public override string BackgroundImage {get; set;} = "/images/Cooles_Bild.jpg";
    [Element]
    public Rectangle Rect {get; set;}
    [Element]
    public Rectangle Rect2{get; set;}
    [Element]
    public Text TheText {get; set;} 
    [Element]
    public Text TheText2 {get; set;}
    [Element]
    public Text LifeText {get; set;}
    [Element]
    public Text TimeText {get; set;}
    public GameObjectContainer<Rectangle> Rects {get; set;} = new();
    public GameObjectContainer<Rectangle> Rects2 {get; set;} = new();
    public GameObjectContainer<Text> TheTexts {get; set;} = new();
    public GameObjectContainer<Text> TheTexts2 {get; set;} = new();
    public GameObjectContainer<Text> LifeTexts {get; set;} = new();
    public GameObjectContainer<Text> TimeTexts {get; set;} = new();

    public BiologyMinigame()
    {
        TimeText = new()
        {
            InnerText = "Time: " + seconds,
            X = 1500,
            Y = 100,
            FontSize = 75,
            FontFamily = "sans-serif",
            Fill = "red", 
        };
        TimeTexts.Add(TimeText);

        LifeText = new()
        {
            InnerText = "Lives: " + lives,
            X = 100,
            Y = 100,
            FontSize = "75",
            FontFamily = "sans-serif",
            Fill = "red" 
        };
        LifeTexts.Add(LifeText);

        TheText = new()
        {
            InnerText = "Richtig",
            X = 300,
            Y = 1000,
            FontSize = "100",
            FontFamily = "sans-serif",
            Fill = "white" 
        };
        TheTexts.Add(TheText);
        
        TheText2 = new()
        {
            InnerText = "Falsch",
            X = 1100,
            Y = 1000,
            FontSize = "100",
            FontFamily = "sans-serif",
            Fill = "white"
        };
        TheTexts2.Add(TheText2);
        
        Rect = new ()
        {
            X = 200,
            Y = 800,
            Width = 500,
            Height = 300,
            Fill = "green",
        };
        Rects.Add(Rect);
        
        Rect2 = new ()
        {
            X = 1000,
            Y = 800,
            Width = 500,
            Height = 300,
            Fill = "red",
        };
        Rects.Add(Rect2);
    }
}