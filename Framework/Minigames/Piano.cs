namespace Framework.Minigames.MinigameDefClasses;

public class PianoMinigame : MinigameDefBase{
    int Pos = 128;
    public override string BackgroundImage {get; set;} = "images/Music/Music_Classroom.jpg";

    public void Move(EventArgs args)
{
    Rect.X += 10;
    Update();
}

        Rectangle Rect = new()
        {
            X = 100,
              Y = 100,
              Width = 100,
              Height = 100,
              Fill = "red",
              OnClick = Move
        };
}
