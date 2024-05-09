using System.Drawing;

namespace Framework.Minigames.MinigameDefClasses;

public class MyMinigame1 : MinigameDefBase
{
    public override string BackgroundImage { get; set; } = "/images/FIGHTER.png";



    public Rectangle? AttackButton1 { get; set; }
    public Rectangle? AttackButton2 { get; set; }
    public Rectangle? StatusButton { get; set; }
    public Rectangle? HealButton { get; set; }
    [Element]
    public Rectangle? HealthBar { get; set; }
    [Element]
    public Rectangle? Villan_HealthBar { get; set; }
    public GameObjectContainer<Rectangle> buttons { get; set; } = new(); //pokemon UI 
    public GameObjectContainer<Rectangle> moving_rects { get; set; } = new(); //bilder 
    public GameObjectContainer<Rectangle> decoration { get; set; } = new();

    public int VillanHealth = 100;
    public int PlayerHealth = 100;
    public bool TaskComplete = true;
    public int AttackBuff = 1;
    public override async Task GameLoop(CancellationToken ct)
    {
        while (true)
        {
            ct.ThrowIfCancellationRequested();

            Health_Bar();
            Villan_Health_Bar();
            await Task.Delay(100, ct);
            VillanAttack();
            TaskComplete = false;
            Update();
            await Task.Delay(4000);
        }

    }
    async public void Villan_Health_Bar()
    {
        Villan_HealthBar.Width = VillanHealth * 10;
        Update();
        if (VillanHealth < 0)
        {
            VillanHealth = 0;
            Villan_HealthBar.Width = 0;
        }

    }
    async public void Health_Bar()
    {
        HealthBar.Width = PlayerHealth * 10;
        Update();
        switch (PlayerHealth)
        {
            case < 0:
                PlayerHealth = 0;
                HealthBar.Width = 0;
                break;
            case <= 25:
                HealthBar.Fill = "Red";  // Covers 0 to 25
                break;
            case <= 50:
                HealthBar.Fill = "Yellow";  // Covers 26 to 50
                break;
            default:
                HealthBar.Fill = "Green";
                //healthbar grün
                break;
        }


    }
    async public void Healing()
    {


        BackgroundImage = "/images/calculator.png";
        Update();
        await Task.Delay(2000);
        BackgroundImage = "/images/FIGHTER.png";
        Update();
        PlayerHealth = PlayerHealth + 25;
        if (PlayerHealth > 100)
        {
            PlayerHealth = 100;
        }
        TaskComplete = true;
    }
    async public void VillanAttack()
    {
        if (TaskComplete == true)
        {
            var rand = new Random();
            BackgroundImage = "/images/calculator.png";
            Update();
            PlayerHealth -= rand.Next(10, 21);
            await Task.Delay(2000);
            Update();
            BackgroundImage = "/images/FIGHTER.png";
            Update();

        }




    }

    async public void Attack_1()
    {

        BackgroundImage = "/images/calculator.png";
        Update();
        await Task.Delay(2000);
        BackgroundImage = "/images/FIGHTER.png";
        Update();
        //VillanHealth = VillanHealth - 10 * AttackBuff;
        PlayerHealth = PlayerHealth - 10 * AttackBuff;
        AttackBuff = 1;
        TaskComplete = true;
    }

    async public void Attack_2()
    {

        BackgroundImage = "/images/calculator.png";
        Update();
        await Task.Delay(2000);
        BackgroundImage = "/images/FIGHTER.png";
        Update();
        VillanHealth = VillanHealth - 10 * AttackBuff;
        AttackBuff = 1;
        TaskComplete = true;
    }
    async public void Status_Attack()
    {

        BackgroundImage = "/images/calculator.png";
        Update();
        await Task.Delay(2000);
        BackgroundImage = "/images/FIGHTER.png";
        Update();
        AttackBuff = 2;
        TaskComplete = true;

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
                Attack_1();
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
                Attack_2();
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
                Status_Attack();
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
                Healing();
                Update();
            }
        };

        moving_rects.Add(HealButton);
        AddElement(HealButton);
        HealthBar = new()
        {
            Id = "heatlthbar4252515",
            X = 400,
            Y = 150,
            Width = PlayerHealth * 10,
            Height = 100,
            Fill = "green"
        };

        Villan_HealthBar = new()
        {
            Id = "Villanheatlthbar4252564642515",
            X = 400,
            Y = 50,
            Width = VillanHealth * 10,
            Height = 100,
            Fill = "red"
        };
    }
}