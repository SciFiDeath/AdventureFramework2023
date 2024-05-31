namespace Framework.Minigames.MinigameDefClasses;

public class PianoMinigame : MinigameDefBase{
    bool Set  = false;
    string[] Colors = {"red","orange","yellow","green","blue","indigo","violet"};
    public override string BackgroundImage {get; set;} = "images/Music/Music_Classroom.jpg";


    char[][] rounds = [['C','E','G'],['F','F','B','A'],['A','B','C','D']];
    bool LevelCompleted = false;
    
    List<char> PressedKeys = new List<char>();
    int level = 1;
    int step = 1;


    public PianoMinigame(){
         Image Start = new()
        {
            ImagePath = "images/Music/Note.png",
            X = 50,
              Y = 25,
              Width = 200,
              Height = 200,
              OnClick = StartGame 
        };
        AddElement(Start);

        Text Welcome = new()
        {
            InnerText = "Welcome to \"Musician Test\"!",
            X = 325,
            Y = 100,
            FontSize = 90,
            Fill = "Blue"
        };
        AddElement(Welcome);

        
    }

    
    async public void CountDown()
{

    Text CountDown = new()
        {
            Id = "CountDownTimer",
            InnerText = "3",
            X = 700,
            Y = 200,
            FontSize = 70,
            Fill = "red"
        };
    AddElement(CountDown);
    Update();
    await Task.Delay(2000);
    CountDown.InnerText = "2";
    Update();
    await Task.Delay(2000);
    CountDown.InnerText = "1";  
    Update();
    await Task.Delay(2000);
    CountDown.InnerText = "Listen!";
    Update();
    Elements.KillId("CountDownTimer");
    Update();
}

    async public void MusicLoop(){
    while(true){
    if(LevelCompleted == true)
        int counter = 0;
        while (counter<rounds[level].Length)
        {
            PlaySound(rounds[level-1][counter]); 
            await Task.Delay(2000);
            counter++;
        }
    Update();
    }
}

    public void StartGame(EventArgs args){
        LevelCompleted = true;
        DrawKeys(args);
        CountDown();
        MusicLoop();
    }
    public void DrawKeys(EventArgs args){

        var C = new Polygon()
        {
             Id = "Key_C",
                   Points = [[770,832],[792,832],[794,945],[793,967],[768,967],[768,949],[770,833]],
                   Fill = Colors[0],
                   FillOpacity=0.3,
                   OnClick = (args) => {


                       if('C' == rounds[level-1][step-1]){
                        if(step == rounds[level-1].Length)
                        {
                            PlaySound('C');
                            Thread.Sleep(1000);
                            PlaySound('S');
                            PressedKeys.Clear();
                            LevelCompleted = true;
                            level++;
                            step = 1;
                        }
                        else{
                        PlaySound('C');
                        PressedKeys.Add('C');
                        step++;
                        }
                       }
                       else{
                        PlaySound('W');
                        step = 1;
                       }
                   }

        };
        AddElement(C);

        var D = new Polygon()
        {
             Id = "Key_D",
                   Points = [[794,966],[796,948],[794,907],[791,833],[816,833],[820,950],[820,966]],
                   Fill = Colors[1],
                   FillOpacity=0.3,
                   OnClick = (args) => {


                       if('D' == rounds[level-1][step-1]){
                           
                        if(step == rounds[level-1].Length)
                        {
                            PlaySound('D');
                            Thread.Sleep(1000);
                            PlaySound('S');
                            PressedKeys.Clear();
                            LevelCompleted = true;
                            level++;
                            step = 1;
                        }
                        else{
                        PlaySound('D');
                        PressedKeys.Add('D');
                        step++;
                        }
                       }
                       else{
                        PlaySound('W');
                        step = 1;
                       }
                   }

        };
        AddElement(D);
        Update();
        

        var E = new Polygon()
        {
             Id = "Key_E",
                   Points = [[841,833],[847,950],[846,966],[821,965],[821,950],[817,832]],
                   Fill = Colors[2],
                   FillOpacity=0.3,
                   OnClick = (args) => {


                       if('E' == rounds[level-1][step-1]){
                           
                        if(step == rounds[level-1].Length)
                        {
                            PlaySound('E');
                            Thread.Sleep(1000);
                            PlaySound('S');
                            PressedKeys.Clear();
                            LevelCompleted = true;
                            level++;
                            step = 1;
                        }
                        else{
                        PlaySound('E');
                        PressedKeys.Add('E');
                        step++;
                        }
                       }
                       else{
                        PlaySound('W');
                        step = 1;
                       }
                   }


        };
        AddElement(E);
        Update();


        var F = new Polygon()
        {
             Id = "Key_F",
                   Points = [[874,949],[873,966],[847,966],[849,950],[841,833],[867,832],[873,920]],
                   Fill = Colors[3],
                   FillOpacity=0.3,
                   OnClick = (args) => {


                       if('F' == rounds[level-1][step-1]){
                           
                        if(step == rounds[level-1].Length)
                        {
                            PlaySound('F');
                            Thread.Sleep(1000);
                            PlaySound('S');
                            PressedKeys.Clear();
                            LevelCompleted = true;
                            level++;
                            step = 1;
                        }
                        else{
                        PlaySound('F');
                        PressedKeys.Add('F');
                        step++;
                       }
                    }
                       else{
                        PlaySound('W');
                        step = 1;
                       }
                   }

        };
        AddElement(F);
        Update();


        var G = new Polygon()
        {
             Id = "Key_G",
                   Points = [[876,950],[874,967],[900,967],[902,950],[890,831],[867,831]],
                   Fill = Colors[4],
                   FillOpacity=0.3,
                   OnClick = (args) => {


                       if('G' == rounds[level-1][step-1]){
                           
                        if(step == rounds[level-1].Length)
                        {
                            PlaySound('G');
                            Thread.Sleep(1000);
                            PlaySound('S');
                            PressedKeys.Clear();
                            LevelCompleted = true;
                            level++;
                            step = 1;
                        }
                        else{
                        PlaySound('G');
                        PressedKeys.Add('G');
                        step++;
                       }
                       }
                       else{
                        PlaySound('W');
                        step = 1;
                       }
                   }

        };
        AddElement(G);
        Update();
        

        var A = new Polygon()
        {
             Id = "Key_A",
                   Points = [[902,950],[901,967],[928,967],[929,950],[914,831],[888,833]],
                   Fill = Colors[5],
                   FillOpacity=0.3,
                   OnClick = (args) => {


                       if('A' == rounds[level-1][step-1]){
                           
                        if(step == rounds[level-1].Length)
                        {
                            PlaySound('A');
                            Thread.Sleep(1000);
                            PlaySound('S');
                            PressedKeys.Clear();
                            LevelCompleted = true;
                            level++;
                            step = 1;
                        }
                        else{
                        PlaySound('A');
                        PressedKeys.Add('A');
                        step++;
                       }
                       }
                       else{
                        PlaySound('W');
                        step = 1;
                       }
                   }

        };
        AddElement(A);
        Update();


        var B = new Polygon()
        {
             Id = "Key_B",
                   Points = [[929,949],[927,966],[953,965],[955,949],[936,832],[914,833]],
                   Fill = Colors[6],
                   FillOpacity=0.3,
                   OnClick = (args) => {


                       if('B' == rounds[level-1][step-1]){
                           
                        if(step == rounds[level-1].Length)
                        {
                            PlaySound('B');
                            Thread.Sleep(1000);
                            PlaySound('S');
                            PressedKeys.Clear();
                            LevelCompleted = true;
                            level++;
                            step = 1;
                        }
                        else{
                        PlaySound('B');
                        PressedKeys.Add('B');
                        step++;
                        }
                       }
                       else{
                        PlaySound('W');
                        step = 1;
                       }
                   }

        };
        AddElement(B);
        Update();
    }

     public void PlaySound(char Note)
    {

        SoundService.PlaySound("Music_Assets/"+Note+".wav");

    }
}

