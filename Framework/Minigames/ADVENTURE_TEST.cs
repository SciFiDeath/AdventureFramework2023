//add crit 1/1000 for enemy because it's funny
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace Framework.Minigames.MinigameDefClasses;

public class MyMinigame1 : MinigameDefBase
{
    public override string BackgroundImage { get; set; } = "/images/Edited/FightPoitionToPunch_2.png";


    public Text? Infotext { get; set; }
    public Text? Infotext2 { get; set; }
    public Text? Infotext3 { get; set; }
    public Text? Infotext4 { get; set; }
    public Text? Infotext5 { get; set; }
    public Text? Infotext6 { get; set; }
    public Text? Infotext7 { get; set; }
    public Text? Infotext8 { get; set; }
    public Rectangle? AttackButton1 { get; set; }
    public Rectangle? AttackButton2 { get; set; }
    public Rectangle? StatusButton { get; set; }
    public Rectangle? HealButton { get; set; }
    public Circle? AttackButton1Info { get; set; }
    public Circle? AttackButton2Info { get; set; }
    public Circle? StatusButtonInfo { get; set; }
    public Circle? HealingButtonInfo { get; set; }
    [Element]
    public Rectangle? HealthBar { get; set; }
    [Element]
    public Rectangle? Villan_HealthBar { get; set; }
    public GameObjectContainer<Rectangle> buttons { get; set; } = new(); //pokemon UI 
    public GameObjectContainer<Rectangle> moving_rects { get; set; } = new(); //bilder 
    public GameObjectContainer<Rectangle> decoration { get; set; } = new();

    public GameObjectContainer<Circle> buttons1 { get; set; } = new(); //pokemon UI 
    public GameObjectContainer<Circle> moving_rects1 { get; set; } = new(); //bilder 
    public GameObjectContainer<Circle> decoration1 { get; set; } = new();

    public GameObjectContainer<Text> buttons2 { get; set; } = new(); //pokemon UI 
    public GameObjectContainer<Text> moving_rects2 { get; set; } = new(); //bilder 
    public GameObjectContainer<Text> decoration2 { get; set; } = new();

    public int VillanHealth = 100; //default 500
    public int VillanHealth2 = 100;
    public int PlayerHealth = 100;
    public int PlayerHealth2 = 100;
    public bool TaskComplete = false;
    public int AttackBuff = 1;
    public int HitPropability = 0;
    public int CritPropability = 13;
    public int Critmultiplier = 1;
    public int ChooseText = 0;
    List<List<string>> messages = [
        ["This Attack has an eighty percent chance hitting the enemy it deals five damage times your multiplier"],
        ["This Attack has a thirty percent chance of hitting the enemy it deals twenty times your multiplier"],
        ["This Attack has a ninety percent chance of hitting the enemy it raises your damage multiplier by one this resets after attacking"],
        ["Heal fifty healthpoints"],
        ["The attack missed"],
        ["Player used Kick"],
        ["Player used Schubser"],
        ["Opponent used Default Villainkick"],
        ["Opponent used Default Villainpunch"],
        ["A critical hit"],
        ["Player used a Red Bull"],
        ["Player used strong Punch"],
        ];
    public override async Task GameLoop(CancellationToken ct)
    //public string Attackbutton1Infotext = "This Attack has a ninety percent chance hitting the enemy it deals five times your multiplier";
    //public string Attackbutton2Infotext = "This Attack has a thirty percent chance of hitting the enemy it deals twenty times your multiplier";
    //public string StatusbuttonInfotext = "This Attack has a ninety percent chance of hitting the enemy it raises your damage multiplier by one this resets after attacking";
    //public string HealingbuttonInfotext = "Heal fifty healthpoints";
    {
        while (true)
        {
            ct.ThrowIfCancellationRequested();
            await Task.Delay(100, ct);
            await Task.Delay(2000);
            await VillanAttack();
            Update();
            await Task.Delay(2000);
        }

    }
    public async void Villan_Health_Bar()
    {
        while (VillanHealth2 > VillanHealth)
        {
            VillanHealth2 -= 1;
            Villan_HealthBar.Height = VillanHealth2 * 2;
            Update();
            await Task.Delay(5);
        }
        while (VillanHealth2 < VillanHealth)
        {
            VillanHealth2 += 1;
            Villan_HealthBar.Height = VillanHealth2 * 2;
            Update();
            await Task.Delay(5);
        }

        Update();
        if (VillanHealth < 0)
        {
            VillanHealth = 0;
            Villan_HealthBar.Width = 0;
        }

    }
    async public void Health_Bar()
    {
        while (PlayerHealth2 > PlayerHealth)
        {
            PlayerHealth2 -= 1;
            HealthBar.Width = PlayerHealth2 * 7 / 2;
            Update();
            await Task.Delay(5);
        }
        Update();

        while (PlayerHealth2 < PlayerHealth)
        {
            PlayerHealth2 += 1;
            HealthBar.Width = PlayerHealth2 * 7 / 2;
            Update();
            await Task.Delay(5);
        }
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

        //RequireItem=Id für Red Bulls
        BackgroundImage = "/images/Edited/RedbullFromBoss_2.png";
        Update();
        await Task.Delay(1000);
        BackgroundImage = "/images/Edited/DrinkRedbull.png";
        Update();
        await Task.Delay(1500);
        BackgroundImage = "/images/Edited/FightPoitionToPunch_2.png";
        PlayerHealth = PlayerHealth + 25;
        if (PlayerHealth > 100)
        {
            PlayerHealth = 100;
        }
        Health_Bar();
        Update();
        await Task.Delay(1000);
        TaskComplete = true;
    }
    async public Task VillanAttack()
    {
        if (TaskComplete == true)
        {
            var rand = new Random();

            int picture = rand.Next(0, 1);
            if (picture == 0)
            {
                BackgroundImage = "/images/Edited/KickFromBoss.png";
            }
            else if (picture == 1)
            {
                BackgroundImage = "/images/Edited/PunchFromBoss_1.png";
            }
            Update();
            await Task.Delay(250);
            PlayerHealth -= rand.Next(10, 21);
            Health_Bar();
            Update();
            await Task.Delay(1500);
            BackgroundImage = "/images/Edited/FightPoitionToPunch_2.png";
            AttackButton1.Fill = "blue";
            AttackButton2.Fill = "blue";
            StatusButton.Fill = "blue";
            HealButton.Fill = "blue";
            Update();
            TaskComplete = false;
        }




    }

    async public void Attack_1()
    {

        var rand = new Random();
        BackgroundImage = "/images/Edited/Right_Punch.png";
        Update();
        await Task.Delay(2000);
        BackgroundImage = "/images/Edited/FightPoitionToPunch_2.png";
        Update();
        HitPropability = 1; //rand.Next(1, 10)
        CritPropability = rand.Next(1, 25);
        if (HitPropability < 9)
        {
            if (CritPropability == 13)
            {
                Critmultiplier += 1;
            }
            VillanHealth = VillanHealth - 20 * AttackBuff * Critmultiplier;
            Critmultiplier = 1;
            Villan_Health_Bar();
            Update();
        }
        AttackBuff = 1;
        TaskComplete = true;
    }

    async public void Attack_2()
    {

        var rand = new Random();
        BackgroundImage = "/images/Edited/LeftKickAndRightPunch.png";
        Update();
        await Task.Delay(2000);
        BackgroundImage = "/images/Edited/FightPoitionToPunch_2.png";
        Update();
        HitPropability = rand.Next(1, 10);
        if (HitPropability < 4)
        {
            VillanHealth = VillanHealth - 19 * AttackBuff;
            Villan_Health_Bar();
        }
        AttackBuff = 1;
        TaskComplete = true;
    }
    async public void Status_Attack()
    {

        var rand = new Random();
        BackgroundImage = "/images/Edited/PushBoss.png";
        Update();
        await Task.Delay(2000);
        BackgroundImage = "/images/Edited/FightPoitionToPunch_2.png";
        Update();
        HitPropability = rand.Next(1, 10);
        if (HitPropability < 10)
        {
            AttackBuff += 1;
            Villan_Health_Bar();
        }
        ;
        TaskComplete = true;

    }
    public MyMinigame1()
    {
        AttackButton1Info = new()
        {
            Id = "2ADSFG7301573",
            R = 10,
            CX = 750,
            ZIndex = 0,
            CY = 800,
            PathLength = 50,
            Fill = "red",
            OnClick = async (args) =>
            {
                Infotext5 = new()
                {
                    Id = "23z4546354534t27354542793",
                    InnerText = "80% 5*multiplier",
                    X = 450,
                    Y = 950,
                    DX = 50,
                    DY = 100,
                    ZIndex = 1,
                    Fill = "white",
                    Rotate = 0,
                    TextLength = 300,
                    FontSize = 11,
                    StretchLetters = false,
                    FontFamily = "Goudy Bookletter 1911",
                };
                AddElement(Infotext5);
                Update();
            }
        };
        AddElement(AttackButton1Info);

        AttackButton1 = new()
        {
            Id = "2ADSFG",
            X = 450,
            Y = 800,
            ZIndex = 0,
            Width = 300,
            Height = 100,
            Fill = "blue",
            OnClick = async (args) =>
            {
                if (TaskComplete == false)
                {
                    TaskComplete = true;
                    AttackButton1.Fill = "grey";
                    AttackButton2.Fill = "grey";
                    StatusButton.Fill = "grey";
                    HealButton.Fill = "grey";
                    AttackButton1.Y += 5;
                    Update();
                    await Task.Delay(50);
                    AttackButton1.Y -= 5;
                    Update();
                    Attack_1();
                    Update();
                }
                else if (TaskComplete == true)
                {
                    AttackButton1.Fill = "grey";
                    AttackButton2.Fill = "grey";
                    StatusButton.Fill = "grey";
                    HealButton.Fill = "grey";
                    Update();
                }

            }
        };
        moving_rects.Add(AttackButton1);
        AddElement(AttackButton1);

        AttackButton2Info = new()
        {
            Id = "2ADSFG730157332235423543454723947",
            R = 50,
            CX = 1150,
            CY = 800,
            PathLength = 50,
            Fill = "red",
            OnClick = async (args) =>
            {
                Infotext6 = new()
                {
                    Id = "23z75427434353542353425223532556345693",
                    ContentMode = false,
                    InnerText = "30% 20*multiplier",
                    X = 450,
                    Y = 950,
                    DX = 50,
                    DY = 100,
                    ZIndex = 1,
                    Fill = "white",
                    Rotate = 0,
                    // TextLength = 300
                    FontSize = 11,
                    StretchLetters = false,
                    FontFamily = "Goudy Bookletter 1911",
                };
                AddElement(Infotext6);
                Update();
            }
        };

        AttackButton2 = new()
        {
            Id = "rectbutton55",
            X = 850,
            Y = 800,
            ZIndex = 0,
            Width = 300,
            Height = 100,
            Fill = "blue",
            OnClick = async (args) =>
            {
                if (TaskComplete == false)
                {
                    TaskComplete = true;
                    AttackButton1.Fill = "grey";
                    AttackButton2.Fill = "grey";
                    StatusButton.Fill = "grey";
                    HealButton.Fill = "grey";
                    AttackButton2.Y += 5;
                    Update();
                    await Task.Delay(50);
                    AttackButton2.Y -= 5;
                    Update();
                    Attack_2();
                    Update();
                }
                else if (TaskComplete == true)
                {
                    AttackButton1.Fill = "grey";
                    AttackButton2.Fill = "grey";
                    StatusButton.Fill = "grey";
                    HealButton.Fill = "grey";
                    Update();
                }
            }
        };

        moving_rects.Add(AttackButton2);
        AddElement(AttackButton2);

        StatusButtonInfo = new()
        {
            Id = "2ADSFG7301573324723947",
            R = 50,
            CX = 750,
            CY = 950,
            PathLength = 50,
            Fill = "red",
            OnClick = async (args) =>
            {
                Infotext7 = new()
                {
                    Id = "23z75424575638235235235235793",
                    ContentMode = false,
                    InnerText = "90% multiplier+=1",
                    X = 450,
                    Y = 950,
                    DX = 50,
                    DY = 100,
                    ZIndex = 1,
                    Fill = "white",
                    Rotate = 0,
                    //TextLength = 300,
                    FontSize = 11,
                    StretchLetters = false,
                    FontFamily = "Goudy Bookletter 1911",
                };
                AddElement(Infotext7);
                Update();
            }
        };

        StatusButton = new()
        {
            Id = "statusrect2345151",
            X = 450,
            Y = 950,
            Width = 300,
            Height = 100,
            ZIndex = 0,
            Fill = "blue",
            OnClick = async (args) =>
            {
                if (TaskComplete == false)
                {
                    TaskComplete = true;
                    AttackButton1.Fill = "grey";
                    AttackButton2.Fill = "grey";
                    StatusButton.Fill = "grey";
                    HealButton.Fill = "grey";
                    StatusButton.Y += 5;
                    Update();
                    await Task.Delay(50);
                    StatusButton.Y -= 5;
                    Update();
                    Status_Attack();
                    Update();
                }
                else if (TaskComplete == true)
                {
                    AttackButton1.Fill = "grey";
                    AttackButton2.Fill = "grey";
                    StatusButton.Fill = "grey";
                    HealButton.Fill = "grey";
                    Update();
                }
            }
        };

        moving_rects.Add(StatusButton);
        AddElement(StatusButton);

        Infotext = new()
        {
            Id = "23z75425647563341525235234323423445654793",
            ContentMode = false,
            InnerText = "Schubser",
            X = 450,
            Y = 950,
            DX = 50,
            DY = 100,
            ZIndex = 1,
            Fill = "white",
            Rotate = 0,
            TextLength = 300,
            FontSize = 11,
            StretchLetters = false,
            FontFamily = "Goudy Bookletter 1911",
        };
        AddElement(Infotext);
        Update();

        Infotext2 = new()
        {
            Id = "23z7544566723235235235345121534552793",
            ContentMode = false,
            InnerText = "RedBull",
            X = 850,
            Y = 950,
            DX = 50,
            DY = 100,
            ZIndex = 1,
            Fill = "white",
            Rotate = 0,
            TextLength = 300,
            FontSize = 11,
            StretchLetters = false,
            FontFamily = "Goudy Bookletter 1911",
        };
        AddElement(Infotext2);
        Update();

        Infotext3 = new()
        {
            Id = "22352533z7542512412423423145431256z5472793",
            ContentMode = false,
            InnerText = "Kick",
            X = 450,
            Y = 800,
            DX = 50,
            DY = 100,
            ZIndex = 1,
            Fill = "white",
            Rotate = 0,
            TextLength = 300,
            FontSize = 11,
            StretchLetters = false,
            FontFamily = "Goudy Bookletter 1911",
        };
        AddElement(Infotext3);
        Update();

        Infotext4 = new()
        {
            Id = "143152352125t2362451413z75434557656442793",
            ContentMode = false,
            InnerText = "Strong Punch",
            X = 850,
            Y = 800,
            DX = 50,
            DY = 100,
            ZIndex = 1,
            Fill = "white",
            Rotate = 0,
            TextLength = 300,
            FontSize = 11,
            StretchLetters = false,
            FontFamily = "Goudy Bookletter 1911",
        };
        AddElement(Infotext4);
        Update();


        HealingButtonInfo = new()
        {
            Id = "2ADSFG730157332472394716347576914365",
            R = 50,
            CX = 1150,
            CY = 950,
            PathLength = 50,
            Fill = "red",
            OnClick = async (args) =>
            {
                Infotext8 = new()
                {
                    Id = "23z252453754223472312543145134465t5243593",
                    ContentMode = false,
                    InnerText = "Heal 50",
                    X = 450,
                    Y = 950,
                    DX = 50,
                    DY = 100,
                    ZIndex = 1,
                    Fill = "white",
                    Rotate = 0,
                    TextLength = 300,
                    FontSize = 11,
                    StretchLetters = false,
                    FontFamily = "Goudy Bookletter 1911",
                };
                AddElement(Infotext8);
                Update();
            }
        };

        HealButton = new()
        {
            Id = "healrect777151",
            X = 850,
            Y = 950,
            ZIndex = 0,
            Width = 300,
            Height = 100,
            Fill = "blue",
            OnClick = async (args) =>
            {
                if (TaskComplete == false)
                {
                    TaskComplete = true;
                    AttackButton1.Fill = "grey";
                    AttackButton2.Fill = "grey";
                    StatusButton.Fill = "grey";
                    HealButton.Fill = "grey";

                    HealButton.Y += 5;
                    Update();
                    await Task.Delay(50);
                    HealButton.Y -= 5;
                    Update();
                    Healing();
                    Update();

                }
                else if (TaskComplete == true)
                {
                    AttackButton1.Fill = "grey";
                    AttackButton2.Fill = "grey";
                    StatusButton.Fill = "grey";
                    HealButton.Fill = "grey";
                    Update();
                }
            }
        };

        moving_rects.Add(HealButton);
        AddElement(HealButton);
        HealthBar = new()
        {
            Id = "heatlthbar4252515",
            X = 50,
            Y = 750,
            Width = PlayerHealth * 7 / 2,
            Height = 35,
            Fill = "green"
        };

        Villan_HealthBar = new()
        {
            Id = "Villanheatlthbar4252564642515",
            X = 1450,
            Y = 50,
            Width = 50,
            Height = VillanHealth * 2,
            Fill = "red"
        };
    }
}