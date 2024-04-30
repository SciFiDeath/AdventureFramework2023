using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading.Tasks.Dataflow;

namespace Framework.Minigames.MinigameDefClasses;

public class MyMinigame1 : MinigameDefBase
{
    public override string BackgroundImage { get; set; } = "/images/FIGHTER.png";



    public Rectangle? AttackButton1 { get; set; }
    public Rectangle? AttackButton2 { get; set; }
    public Rectangle? StatusButton { get; set; }
    public Rectangle? HealButton { get; set; }
    public GameObjectContainer<Rectangle> buttons { get; set; } = new(); //pokemon UI 
    public GameObjectContainer<Rectangle> moving_rects { get; set; } = new(); //bilder 
    public GameObjectContainer<Rectangle> decoration { get; set; } = new();

    public int VillianHealth = 100;
    public int PlayerHealth = 100;
    public bool TaskComplete = true;
    public override Task GameLoop(CancellationToken ct)
    {
        while (true)
        {
            ct.ThrowIfCancellationRequested();
        }

    }
    async public void Healing()
    {
        if (TaskComplete == true)
        {
            BackgroundImage = "/images/calculator.png";
            Update();
            await Task.Delay(2000);
            BackgroundImage = "/images/FIGHTER.png";
            PlayerHealth = PlayerHealth + 25;
            if (PlayerHealth > 100)
            {
                PlayerHealth = 100;
            }
        }
    }
    public MyMinigame1()
    {
        AttackButton1 = new()
        {
            Id = "2ADSFG",
            X = 400,
            Y = 800,
            Width = 300,
            Height = 100,
            Fill = "blue",
            OnClick = async (args) =>
            {
                AttackButton1.Y += 5;
                Update();
                await Task.Delay(50);
                AttackButton1.Y -= 5;
                Update();
            }


        };
        moving_rects.Add(AttackButton1);
        AddElement(AttackButton1);



        AttackButton2 = new()
        {
            Id = "rectbutton55",
            X = 800,
            Y = 800,
            Width = 300,
            Height = 100,
            Fill = "blue",
            OnClick = async (args) =>
            {
                AttackButton2.Y += 5;
                Update();
                await Task.Delay(50);
                AttackButton2.Y -= 5;
                Update();
            }
        };

        moving_rects.Add(AttackButton2);
        AddElement(AttackButton2);

        StatusButton = new()
        {
            Id = "statusrect2345151",
            X = 400,
            Y = 950,
            Width = 300,
            Height = 100,
            Fill = "blue",
            OnClick = async (args) =>
            {
                StatusButton.Y += 5;
                Update();
                await Task.Delay(50);
                StatusButton.Y -= 5;
                Update();
            }
        };

        moving_rects.Add(StatusButton);
        AddElement(StatusButton);

        HealButton = new()
        {
            Id = "healrect777151",
            X = 800,
            Y = 950,
            Width = 300,
            Height = 100,
            Fill = "blue",
            OnClick = async (args) =>
            {
                HealButton.Y += 5;
                Update();
                await Task.Delay(50);
                HealButton.Y -= 5;
                Update();
                Healing(); // TODO: HP Leiste erstellen, HP Leiste durch 3 Funktionen (PlayerHealing, PlayerTakenDMG und VillianTakenDMG) manipulierbar sein, for schleife nutzen und Playerhealth zbs als grund wert nehmen
                Update();
            }
        };

        moving_rects.Add(HealButton);
        AddElement(HealButton);
    }
}