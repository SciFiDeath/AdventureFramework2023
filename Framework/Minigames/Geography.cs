using System.Security.Cryptography.X509Certificates;

namespace Framework.Minigames.MinigameDefClasses;

public class Geography : MinigameDefBase
{
    public override string BackgroundImage { get; set; } = "minigame_assets/Minigame_Geographie/Minigame_Geographie.JPG";
    [Element]
    public Rectangle Rect { get; set; } = new();
    [Element]
    public Rectangle Qrect { get; set; } = new();
    [Element]
    public Rectangle Rect2 { get; set; } = new();
    [Element]
    public Rectangle Rect3 { get; set; } = new();
    [Element]
    public Rectangle Rect4 { get; set; } = new();
    [Element]
    public Text Question { get; set; }
    [Element]
    public Text Answer1 { get; set; }
    [Element]
    public Text Answer2 { get; set; }
    [Element]
    public Text Answer3 { get; set; }
    [Element]
    public Text Answer4 { get; set; }
    [Element]
    public Text lifes1 { get; set; }
    public GameObjectContainer<Text> TheTexts { get; set; } = new();
    public int round = 0;
    public int lifes = 2;
    public string[] questions = {
            "What is the capital of Canada?",
            "Which river is the longest in the world?",
            "Which volcanic rock is black and glassy?",
            "What is the name of the dry wind that blows from the Alps?",
            "Which biome is characterized by permafrost?"
        };
    public string[] correctAnswers = { "Ottawa", "Nile", "Obsidian", "Foehn", "Tundra" };
    public int[] correctAnswersnum = { 3, 2, 3, 4, 4 };
    public string[] answers1 = { "Toronto", "Amazon", "Pumice", "Sirocco", "Desert" };
    public int[] answers1num = { 1, 1, 1, 1, 1 };
    public string[] answers2 = { "Vancouver", "Nile", "Granite", "Mistral", "Rainforest" };
    public int[] answers2num = { 2, 2, 2, 2, 2 };
    public string[] answers3 = { "Ottawa", "Yangtze", "Obsidian", "Chinook", "Savanna" };
    public int[] answers3num = { 3, 3, 3, 3, 3 };
    public string[] answers4 = { "Montreal", "Mississippi", "Basalt", "Foehn", "Tundra" };
    public int[] answers4num = { 4, 4, 4, 4, 4 };

    public Geography()
    {
        Question = new()
        {
            InnerText = questions[round],
            X = 100,
            Y = 200,
            FontSize = 60,
            FontFamily = "sans-serif",
            Fill = "black"
        };
        lifes1 = new()
        {
            InnerText = "lifes: " + lifes.ToString(),
            X = 1300,
            Y = 400,
            FontSize = 100,
            FontFamily = "sans-serif",
            Fill = "black",
        };


        Answer1 = new()
        {
            InnerText = answers1[round],
            X = 200,
            Y = 630,
            FontSize = 100,
            FontFamily = "sans-serif",
            Fill = "black",
            OnClick = (args) =>
            {
                if (lifes != 0)
                {
                    if (correctAnswersnum[round] == answers1num[round])
                    {
                        round += 1;
                        Question.InnerText = questions[round];
                        Answer1.InnerText = answers1[round];
                        Answer2.InnerText = answers2[round];
                        Answer3.InnerText = answers3[round];
                        Answer4.InnerText = answers4[round];
                        Update();
                    }
                    else if (correctAnswersnum[round] != answers1num[round])
                    {
                        lifes -= 1;
                        lifes1.InnerText = "lifes: " + lifes.ToString();
                        Update();
                    }
                }
            }
        };

        Answer2 = new()
        {
            InnerText = answers2[round],
            X = 1000,
            Y = 630,
            FontSize = 100,
            FontFamily = "sans-serif",
            Fill = "black",
            OnClick = (args) =>
            {
                if (lifes != 0)
                {
                    if (correctAnswersnum[round] == answers2num[round])
                    {
                        round += 1;
                        Question.InnerText = questions[round];
                        Answer1.InnerText = answers1[round];
                        Answer2.InnerText = answers2[round];
                        Answer3.InnerText = answers3[round];
                        Answer4.InnerText = answers4[round];
                        Update();
                    }
                    else if (correctAnswersnum[round] != answers2num[round])
                    {
                        lifes -= 1;
                        lifes1.InnerText = "lifes: " + lifes.ToString();
                        Update();
                    }
                }
            }
        };

        Answer3 = new()
        {
            InnerText = answers3[round],
            X = 200,
            Y = 820,
            FontSize = 100,
            FontFamily = "sans-serif",
            Fill = "black",
            OnClick = (args) =>
            {
                if (lifes != 0)
                {
                    if (correctAnswersnum[round] == answers3num[round])
                    {
                        round += 1;
                        Question.InnerText = questions[round];
                        Answer1.InnerText = answers1[round];
                        Answer2.InnerText = answers2[round];
                        Answer3.InnerText = answers3[round];
                        Answer4.InnerText = answers4[round];
                        Update();
                    }
                    else if (correctAnswersnum[round] != answers3num[round])
                    {
                        lifes -= 1;
                        lifes1.InnerText = "lifes: " + lifes.ToString();
                        Update();
                    }
                }
            }
        };

        Answer4 = new()
        {
            InnerText = answers4[round],
            X = 1000,
            Y = 820,
            FontSize = 100,
            FontFamily = "sans-serif",
            Fill = "black",
            OnClick = (args) =>
            {
                if (lifes != 0)
                {
                    if (correctAnswersnum[round] == answers4num[round])
                    {
                        round += 1;
                        Question.InnerText = questions[round];
                        Answer1.InnerText = answers1[round];
                        Answer2.InnerText = answers2[round];
                        Answer3.InnerText = answers3[round];
                        Answer4.InnerText = answers4[round];
                        Update();
                    }
                    else if (correctAnswersnum[round] != answers4num[round])
                    {
                        lifes -= 1;
                        lifes1.InnerText = "lifes: " + lifes.ToString();
                        Update();
                    }
                }
            }
        };

        Qrect = new()
        {
            X = 0,
            Y = 80,
            Width = 1700,
            Height = 200,
            Fill = "white",

        };

        Rect = new()
        {
            X = 1,
            Y = 500,
            Width = 820,
            Height = 200,
            Fill = "red",
            OnClick = (args) =>
            {
                if (lifes != 0)
                {
                    if (correctAnswersnum[round] == answers1num[round])
                    {
                        round += 1;
                        Question.InnerText = questions[round];
                        Answer1.InnerText = answers1[round];
                        Answer2.InnerText = answers2[round];
                        Answer3.InnerText = answers3[round];
                        Answer4.InnerText = answers4[round];
                        Update();
                    }
                    else if (correctAnswersnum[round] != answers1num[round])
                    {
                        lifes -= 1;
                        lifes1.InnerText = "lifes: " + lifes.ToString();
                        Update();
                    }
                }
            }


        };

        Rect2 = new()
        {
            X = 815,
            Y = 500,
            Width = 830,
            Height = 200,
            Fill = "green",
            OnClick = (args) =>
            {
                if (lifes != 0)
                {
                    if (correctAnswersnum[round] == answers2num[round])
                    {
                        round += 1;
                        Question.InnerText = questions[round];
                        Answer1.InnerText = answers1[round];
                        Answer2.InnerText = answers2[round];
                        Answer3.InnerText = answers3[round];
                        Answer4.InnerText = answers4[round];
                        Update();
                    }
                    else if (correctAnswersnum[round] != answers2num[round])
                    {
                        lifes -= 1;
                        lifes1.InnerText = "lifes: " + lifes.ToString();
                        Update();
                    }
                }
            }
        };

        Rect3 = new()
        {
            X = 1,
            Y = 700,
            Width = 820,
            Height = 200,
            Fill = "blue",
            OnClick = (args) =>
            {
                if (lifes != 0)
                {
                    if (correctAnswersnum[round] == answers3num[round])
                    {
                        round += 1;
                        Question.InnerText = questions[round];
                        Answer1.InnerText = answers1[round];
                        Answer2.InnerText = answers2[round];
                        Answer3.InnerText = answers3[round];
                        Answer4.InnerText = answers4[round];
                        Update();
                    }
                    else if (correctAnswersnum[round] != answers3num[round])
                    {
                        lifes -= 1;
                        lifes1.InnerText = "lifes: " + lifes.ToString();
                        Update();
                    }
                }
            }
        };

        Rect4 = new()
        {
            X = 815,
            Y = 700,
            Width = 810,
            Height = 200,
            Fill = "yellow",
            OnClick = (args) =>
            {
                if (lifes != 0)
                {
                    if (correctAnswersnum[round] == answers4num[round])
                    {
                        round += 1;
                        Question.InnerText = questions[round];
                        Answer1.InnerText = answers1[round];
                        Answer2.InnerText = answers2[round];
                        Answer3.InnerText = answers3[round];
                        Answer4.InnerText = answers4[round];
                        Update();
                    }
                    else if (correctAnswersnum[round] != answers4num[round])
                    {
                        lifes -= 1;
                        lifes1.InnerText = "lifes: " + lifes.ToString();
                        Update();
                    }
                }
            }
        };

    }

}